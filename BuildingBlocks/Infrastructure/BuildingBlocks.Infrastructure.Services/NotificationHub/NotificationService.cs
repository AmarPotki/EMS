using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildingBlocks.Infrastructure.Services.NotificationHub
{
    public class NotificationService : INotificationService
    {
        private NotificationHubClient _hub;
        private readonly string _notificationHubName = "Irvine-Mobile";
        private readonly string _notificationHubConnection = "Endpoint=sb://vielit-test.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=kj7h8GLR4y0/I4h0JKiJBtXu76LNAIZ3EddWWE0NmHg=";

        public NotificationService()
        {
            //_hub = NotificationHubClient
            //.CreateClientFromConnectionString(_notificationHubConnection, _notificationHubName);
        }
        public async Task SendNotification(string message, string data)
        {
            var jGcmMessage = "{\"data\":{\"message\":\"" + message + "\",\"data\":" + data + "}}";
            //var gcmResult = await _hub.SendGcmNativeNotificationAsync(jGcmMessage);
            var jIosMessage = "{\"aps\":{\"alert\":\"" + message + "\",\"data\":" + data + "}}";
            var iosResult = await _hub.SendAppleNativeNotificationAsync(jIosMessage);
        }

        public async Task SendNotification(string message, string data, IEnumerable<string> tags)
        {
            var jGcmMessage = "{\"data\":{\"message\":\"" + message + "\",\"data\":" + data + "}}";
            //var gcmResult = await _hub.SendGcmNativeNotificationAsync(jGcmMessage,tags);
            var jIosMessage = "{\"aps\":{\"alert\":\"" + message + "\"}}";
            var iosResult = await _hub.SendAppleNativeNotificationAsync(jIosMessage, tags);
        }

        public async Task SendNotificationRest(string message, string data)
        {
            await SendNativeNotificationREST(_notificationHubName, _notificationHubConnection, message, "GCM", data);
        }

        public async Task SendNotificationRest(string message, string data, IEnumerable<string> tags)
        {
            var strTags = string.Join("||", tags);
            await SendNativeNotificationREST(_notificationHubName, _notificationHubConnection, message, "GCM", data, strTags);
        }
        private static async Task<string> SendNativeNotificationREST(string hubname, string connectionString, string message, string nativeType,
            string data = null, string tags = null)
        {
            ConnectionStringUtility connectionSaSUtil = new ConnectionStringUtility(connectionString);
            string location = null;

            string hubResource = "messages/?";
            string apiVersion = "api-version=2015-04";
            string notificationId = "Failed to get Notification Id";

            //=== Generate SaS Security Token for Authentication header ===
            // Determine the targetUri that we will sign
            string uri = connectionSaSUtil.Endpoint + hubname + "/" + hubResource + apiVersion;

            // 10 min expiration
            string SasToken = connectionSaSUtil.getSaSToken(uri, 10);

            WebHeaderCollection headers = new WebHeaderCollection();
            string body;
            HttpWebResponse response = null;
            if (!string.IsNullOrWhiteSpace(tags))
                headers.Add("ServiceBusNotification-Tags", tags);
            switch (nativeType.ToLower())
            {

                case "apns":
                    headers.Add("ServiceBusNotification-Format", "apple");
                    body = string.IsNullOrWhiteSpace(data) ? "{\"aps\":{\"alert\":\"" + message + "\"}}" :
                       "{\"aps\":{\"alert\":\"" + message + "\",\"data\":" + data + "}}";
                    response = await ExecuteREST("POST", uri, SasToken, headers, body);
                    break;

                case "gcm":
                    headers.Add("ServiceBusNotification-Format", "gcm");
                    body = string.IsNullOrWhiteSpace(data) ? "{\"data\":{\"message\":\"" + message + "\"}}" :
                    "{\"data\":{\"message\":\"" + message + "\",\"data\":" + data + "}}";
                    response = await ExecuteREST("POST", uri, SasToken, headers, body);
                    break;

                case "wns":
                    headers.Add("X-WNS-Type", "wns/toast");
                    headers.Add("ServiceBusNotification-Format", "windows");
                    body = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                            "<toast>" +
                                "<visual>" +
                                    "<binding template=\"ToastText01\">" +
                                        "<text id=\"1\">" +
                                            message +
                                        "</text>" +
                                    "</binding>" +
                                "</visual>" +
                            "</toast>";
                    response = await ExecuteREST("POST", uri, SasToken, headers, body, "application/xml");
                    break;
            }

            char[] seps1 = { '?' };
            char[] seps2 = { '/' };

            if ((int)response.StatusCode != 201)
            {
                return string.Format("Failed to get notification message id - Http Status {0} : {1}", (int)response.StatusCode, response.StatusCode.ToString());
            }

            if ((location = response.Headers.Get("Location")) != null)
            {
                string[] locationUrl = location.Split(seps1);
                string[] locationParts = locationUrl[0].Split(seps2);

                notificationId = locationParts[locationParts.Length - 1];

                return notificationId;
            }
            else
                return null;
        }

        private static async Task<HttpWebResponse> GetNotificationTelemtry(string id, string hubname, string connectionString)
        {
            string hubResource = "messages/" + id + "?";
            string apiVersion = "api-version=2015-04";
            ConnectionStringUtility connectionSasUtil = new ConnectionStringUtility(connectionString);

            //=== Generate SaS Security Token for Authentication header ===
            // Determine the targetUri that we will sign
            string uri = connectionSasUtil.Endpoint + hubname + "/" + hubResource + apiVersion;
            string SasToken = connectionSasUtil.getSaSToken(uri, 60);

            return await ExecuteREST("GET", uri, SasToken);
        }


        private static async Task<string> GetPlatformNotificationServiceFeedbackContainer(string hubName, string connectionString)
        {
            HttpWebResponse response = null;
            ConnectionStringUtility connectionSasUtil = new ConnectionStringUtility(connectionString);

            string hubResource = "feedbackcontainer?";
            string apiVersion = "api-version=2015-04";

            //=== Generate SaS Security Token for Authentication header ===
            // Determine the targetUri that we will sign
            string uri = connectionSasUtil.Endpoint + hubName + "/" + hubResource + apiVersion;

            // 10 min expiration
            string SasToken = connectionSasUtil.getSaSToken(uri, 10);
            response = await ExecuteREST("GET", uri, SasToken);

            if ((int)response.StatusCode != 200)
            {
                // log

                // Get the stream associated with the response.
                Stream errorStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader errorReader = new StreamReader(errorStream, Encoding.UTF8);


                return null;
            }

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            string containerUri = readStream.ReadToEnd();

            readStream.Close();
            receiveStream.Close();
            return containerUri;
        }

        private static async Task WalkBlobContainer(string containerUri)
        {
            string listcontainerUri = containerUri + "&restype=container&comp=list";

            HttpWebResponse response = await ExecuteREST("GET", listcontainerUri, null);

            // Get Blob name
            Stream receiveStreamContainer = null;
            StreamReader readStreamContainer = null;

            if (((int)response.StatusCode == 200) && response.ContentType.Contains("application/xml"))
            {
                // Get the stream associated with the response.
                receiveStreamContainer = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStreamContainer = new StreamReader(receiveStreamContainer, Encoding.UTF8);

                if (readStreamContainer != null)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(readStreamContainer.ReadToEnd());
                    readStreamContainer.Close();
                    receiveStreamContainer.Close();

                    StringBuilder sb = new StringBuilder();
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true,
                        IndentChars = "  ",
                        NewLineChars = "\r\n",
                        NewLineHandling = NewLineHandling.Replace
                    };

                    using (XmlWriter writer = XmlWriter.Create(sb, settings))
                    {
                        xml.Save(writer);
                    }




                    XmlNodeList list = xml.GetElementsByTagName("Blob");

                    string[] parts = null;
                    char[] seps = { '?' };
                    string blobURL = null;

                    foreach (XmlNode node in list)
                    {
                        // Console.WriteLine("Get Blob named : " + node["Name"].InnerText);
                        parts = containerUri.Split(seps);
                        blobURL = parts[0] + "/" + node["Name"].InnerText + "?" + parts[1];
                        // Console.WriteLine("Blob URL : " + blobURL);
                        response = await ExecuteREST("GET", blobURL, null);
                        DisplayResponseBody(response);
                    }
                }
            }
        }



        private static async Task<HttpWebResponse> ExecuteREST(string httpMethod, string uri, string sasToken, WebHeaderCollection headers = null, string body = null, string contentType = "application/json")
        {
            //=== Execute the request 

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            HttpWebResponse response = null;

            request.Method = httpMethod;
            request.ContentType = contentType;
            request.ContentLength = 0;

            if (sasToken != null)
                request.Headers.Add("Authorization", sasToken);

            if (headers != null)
            {
                request.Headers.Add(headers);
            }

            if (body != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(body);

                try
                {
                    request.ContentLength = bytes.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                catch
                {
                    // log
                }
            }

            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
            }
            catch (WebException we)
            {
                if (we.Response != null)
                {
                    response = (HttpWebResponse)we.Response;
                }
                // else
                // log
            }
            catch
            {
                // log
            }

            return response;
        }

        private static void DisplayResponseBody(HttpWebResponse response, string forcedType = null)
        {
            if (response == null)
                return;

            string contentType = response.ContentType;
            if (forcedType != null)
                contentType = forcedType;

            // Get the stream associated with the response.
            var receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            var readStream = new StreamReader(receiveStream, Encoding.UTF8);


            if (receiveStream == null)
                return;



            if (contentType.Contains("application/octet-stream"))
            {
                string xmlFiles = readStream.ReadToEnd();
                string[] sseps = { "<?xml " };
                string[] docs = xmlFiles.Split(sseps, StringSplitOptions.RemoveEmptyEntries);

                StringBuilder sb = null;
                XmlDocument xml = null;
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace
                };

                foreach (string doc in docs)
                {
                    xml = new XmlDocument();
                    xml.LoadXml(sseps[0] + doc);
                    sb = new StringBuilder();

                    using (XmlWriter writer = XmlWriter.Create(sb, settings))
                    {
                        xml.Save(writer);
                    }


                }
            }

            if (contentType.Contains("application/xml"))
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(readStream.ReadToEnd());

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace
                };

                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    xml.Save(writer);
                }


            }

            if (contentType.Contains("application/json"))
            {
                // Console.WriteLine(JsonHelper.FormatJson(readStream.ReadToEnd()));
            }

            readStream.Close();
            receiveStream.Close();
        }

    }
}

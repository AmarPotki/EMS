using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.Services.NotificationHub
{
   public interface INotificationService
    {
        /// <summary>
        /// broadcast
        /// </summary>
        /// <param name="message">the text message for showing to user</param>
        /// <param name="data">it is some extra information that you need pass to client</param>
        /// <returns></returns>
        Task SendNotification(string message, string data);
        /// <summary>
        /// specefic users
        /// </summary>
        /// <param name="message">the text message for showing to user</param>
        /// <param name="data">it is some extra information that you need pass to client</param>
        /// <param name="tags (max 20)">tags is list of device identities</param>
        /// <returns></returns>
        Task SendNotification(string message,string data, IEnumerable<string> tags);
        /// <summary>
        /// broadcast
        /// </summary>
        /// <param name="message">the text message for showing to user</param>
        /// <param name="data">it is some extra information that you need pass to client</param>
        /// <returns></returns>
        Task SendNotificationRest(string message, string data);
        /// <summary>
        /// specefic users or devices
        /// </summary>
        /// <param name="message">the text message for showing to user</param>
        /// <param name="data">it is some extra information that you need pass to client</param>
        /// <param name="tags (max 20)">tags is list of device identities</param>
        /// <returns></returns>
        Task SendNotificationRest(string message, string data, IEnumerable<string> tags);
    }
}

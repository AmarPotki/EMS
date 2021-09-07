using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace EMS.WebSPA.Configuration
{
    public class ServiceConfiguration
    {
        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
              {
                  Name = "role",
                  DisplayName = "Role",
                  Description = "Allow the service access to your user roles.",
                  UserClaims = new[] { JwtClaimTypes.Role, ClaimTypes.Role },
                  ShowInDiscoveryDocument = true,
                  Required = true,
                  Emphasize = true
              },
             new IdentityResource
              {
                  Name = "tenant",
                  DisplayName = "Tenant",
                  Description = "Tenant Information",
                  UserClaims = new[] { JwtClaimTypes.ClientId },
                  ShowInDiscoveryDocument = true,
                  Required = true,
                  Emphasize = true
              }
    };

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("emsapi", "EMS API")
            };
        }

        public static IEnumerable<Client> GetClients(string url)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "spa",
                    ClientName = "EMS SPA WebApp",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        $"https://{url}/",
                        $"https://{url}/",
                        $"https://{url}/",
                        $"https://{url}/"
                    },
                    PostLogoutRedirectUris = {  $"https://{url}/" },
                    AllowedScopes = { "openid", "profile", "email" , "emsapi", "role" },
                    AllowedCorsOrigins =
                    {
                         $"https://{url}/"
                    }
                },  new Client
                {
                    ClientId = "finbuckle",
                    ClientName = "EMS SPA WebApp",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        $"https://finbuckle.{url}/",
                        $"https://finbuckle.{url}/",
                        $"https://finbuckle.{url}/",
                        $"https://finbuckle.{url}/"
                    },
                    PostLogoutRedirectUris = {  $"https://finbuckle.{url}/" },
                    AllowedScopes = { "openid", "profile", "email" , "emsapi", "role" },
                    AllowedCorsOrigins =
                    {
                         $"https://finbuckle.{url}/"
                    }
                },
                new Client
                {
                    ClientId = "initech",
                    ClientName = "EMS initech SPA WebApp",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        $"https://initech.{url}/",
                        $"https://initech.{url}/",
                        $"https://initech.{url}/",
                        $"https://initech.{url}/"
                    },
                    PostLogoutRedirectUris = {  $"https://initech.{url}/" },
                    AllowedScopes = { "openid", "profile", "email" , "emsapi", "role" },
                    AllowedCorsOrigins =
                    {
                         $"https://initech.{url}/"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "818727",
                    Username = "alice",
                    Password = "alice",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser
                {
                    SubjectId = "88421113",
                    Username = "bob",
                    Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }
                }
            };
        }
    }
}

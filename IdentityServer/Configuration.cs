using System;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        //user information
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                //new IdentityResources.Profile(),
                //add custom scopes
                new IdentityResource
                {
                    Name = "ck.scope",
                    UserClaims =
                    {
                        "ck.secret"
                    }
                }
            };

        //defination of api resources
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ApiOne","The First Api"),
                new ApiScope("ApiTwo","The Second Api")
            };


        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource
            {
                Name = "ApiOne",
                Scopes = new List<string>
                {
                    "ApiOne"
                }
            },
             new ApiResource
             {
                Name = "ApiTwo",
                Scopes = new List<string>
                {
                    "ApiTwo"
                }
             }
        };



        //define clients
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = new List<Secret>
                        {
                            new Secret("client_secret".ToSha256())
                        },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = {"ApiOne"}
                },

                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = new List<Secret>
                        {
                            new Secret("client_secret_mvc".ToSha256())
                        },
                    RedirectUris = {"https://localhost:25289/signin-oidc" },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        "ApiOne",
                        "ApiTwo",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        "ck.scope"
                    },



                    //puts all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "client_id_js",
                    RedirectUris = {"https://localhost:46025/home/signin" },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes =
                    {
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        "ApiOne",
                    },
                    AllowAccessTokensViaBrowser = true,

                }
            };

    }
}


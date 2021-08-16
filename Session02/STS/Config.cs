// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;

namespace STS
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("read-diaries"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "mvc-framework",
                    ClientSecrets = { new Secret("mvc-framework-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5055/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5055/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "read-diaries"
                    },
                    RequireConsent = true,
                },
                new Client
                {
                    ClientId = "mvc-raw",
                    ClientSecrets = { new Secret("mvc-raw-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5056/Diaries/Read" },
                    AllowedScopes = new List<string>
                    {
                        "read-diaries"
                    },
                    RequirePkce = false,
                    RequireConsent = true,
                },

                new Client()
                {
                    ClientId = "console-ropc-raw",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequirePkce = false,
                    RequireConsent = false,
                    AllowedScopes = new List<string>
                    {
                        "read-diaries"
                    },
                },

                new Client()
                {
                    ClientId = "console-client-credentials-raw",
                    ClientSecrets = { new Secret("console-client-credentials-raw-secret".Sha256()) },
                    RequireClientSecret = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequirePkce = false,
                    RequireConsent = false,
                    AllowedScopes = new List<string>
                    {
                        "read-diaries"
                    },
                },

                new Client
                {
                    ClientId = "js-implicit-raw",
                    ClientSecrets = { new Secret("js-implicit-raw".Sha256()) },
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:5057/" },
                    AllowedScopes = new List<string>
                    {
                        "read-diaries"
                    },
                    RequirePkce = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                },
            };
    }
}
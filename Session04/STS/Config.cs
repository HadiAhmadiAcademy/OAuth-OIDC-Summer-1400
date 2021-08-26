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
                new IdentityResources.Email(), 
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("read-diaries", "can access to read your diaries"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "mvc-raw",
                    ClientSecrets = { new Secret("mvc-raw-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5056/Login/Callback" },
                    AllowedScopes = new List<string>()
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,
                            "read-diaries"
                        },
                    RequirePkce = false,
                    RequireConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenType = AccessTokenType.Reference,
                },
                new Client
                {
                    ClientId = "mvc-auth-only",
                    ClientSecrets = { new Secret("mvc-auth-only-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:5058/Login/Callback" },
                    AllowedScopes = new List<string>()
                    {
                        "openid",
                        "profile"
                    },
                    RequirePkce = false,
                    RequireConsent = true,
                },
                new Client
                {
                    ClientId = "js-implicit-raw",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:5057/" },
                    AllowedScopes = new List<string>()
                    {
                        "openid",
                        "profile"
                    },
                    RequirePkce = false,
                    RequireConsent = true,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                },

            };
    }
}
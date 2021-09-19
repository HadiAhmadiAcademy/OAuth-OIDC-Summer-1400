// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
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
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "diaries-front",
                    RequireClientSecret = false,    
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "http://localhost:4200/auth-callback" },
                    AllowOfflineAccess = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                    AllowedCorsOrigins = new List<string>()
                    {
                        "http://localhost:4200",
                    },
                },
                new Client
                {
                    ClientId = "todo-app",
                    RequireClientSecret = true,
                    ClientSecrets =  { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5007/signin-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RequireConsent = false,
                    RequirePkce = true,
                },
            };
    }
}
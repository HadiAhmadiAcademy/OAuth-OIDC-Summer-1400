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

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("diaries-api")
                {
                    ApiSecrets = new List<Secret>()
                    {
                        new Secret("diaries-api-secret".Sha256())
                    },
                    Scopes = new List<string>()
                    {
                        "read-diaries"
                    }
                }, 
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("read-diaries"), 
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    Enabled = true,
                    ClientId = "smart-tv",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("smart-tv-secret".Sha256())
                    },
                    RequireClientSecret = true,
                    AllowedGrantTypes = GrantTypes.DeviceFlow,
                    RequirePkce = false,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Reference,
                    AllowedScopes = new List<string>
                    {
                        "read-diaries"
                    },
                },
            };
    }
}
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
                new ApiResource("api1")
                {
                    ApiSecrets = new List<Secret>()
                    {
                        new Secret("api1-secret".Sha256())
                    },
                    Scopes = new List<string>()
                    {
                        "forecasts"
                    }
                },
                new ApiResource("api2")
                {
                    ApiSecrets = new List<Secret>()
                    {
                        new Secret("api2-secret".Sha256())
                    },
                    Scopes = new List<string>()
                    {
                        "locations"
                    }
                },
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("forecasts"), 
                new ApiScope("locations"), 
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    Enabled = true,
                    ClientId = "original-client",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("original-client-secret".Sha256())
                    },
                    RequireClientSecret = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequirePkce = false,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new List<string>
                    {
                        "forecasts"
                    },
                },

                new Client()
                {
                    Enabled = true,
                    ClientId = "api1",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("api1-secret".Sha256())
                    },
                    RequireClientSecret = true,
                    AllowedGrantTypes = new[] {"urn:ietf:params:oauth:grant-type:token-exchange"},
                    RequirePkce = false,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new List<string>
                    {
                        "locations"
                    },
                },
            };
    }
}
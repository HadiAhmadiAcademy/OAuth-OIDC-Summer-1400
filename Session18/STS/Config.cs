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
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("forecasts"), 
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    Enabled = true,
                    ClientId = "client-1",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("client-1-secret".Sha256()),
                    },
                    RequireClientSecret = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
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
                    ClientId = "client-mvc",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(@"4777761f1c63d762b0884ca2a39ef4d5c7b61588", "mtls.test")
                        {
                            Type = IdentityServerConstants.SecretTypes.X509CertificateThumbprint
                        }
                    },
                    RequireClientSecret = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    RedirectUris = new List<string>()
                        {
                        "https://localhost:5056/Forecasts/read"
                        },
                    AllowedScopes = new List<string>
                    {
                        "forecasts"
                    },
                },
            };
    }
}
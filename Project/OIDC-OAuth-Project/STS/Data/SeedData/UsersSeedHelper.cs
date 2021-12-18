using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace STS.Data.SeedData
{
    public static class UsersSeedHelper
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var existingUsers = context.Users.Count();
                if (existingUsers == 0)
                {
                    var seedUsers = GetUsers();
                    foreach (var identityUser in seedUsers)
                    {
                        var result = context.CreateAsync(identityUser, "Pa$$word123").Result;

                    }
                }
            }
        }

        private static List<IdentityUser> GetUsers()
        {
            return new List<IdentityUser>()
            {
                new IdentityUser()
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                },
                new IdentityUser()
                {
                    UserName = "Alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                }
            };
        }
    }
}
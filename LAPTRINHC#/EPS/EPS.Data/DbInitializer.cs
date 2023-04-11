using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EPS.Utils.Common;

namespace EPS.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EPSContext>();

                context.Database.Migrate();

                if (!context.IdentityClients.Any())
                {
                    var sql = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "companys.sql"));
                    context.Database.ExecuteSqlRaw(sql);

                    var privileges = Enum.GetValues(typeof(PrivilegeList)).OfType<PrivilegeList>().Select(x => new Privilege() { Id = x.ToString(), Name = x.GetEnumDescription() });
                    context.Privileges.AddRange(privileges);

                    context.Roles.Add(new Role() { Name = "Administrator" });

                    context.IdentityClients.Add(new Entities.IdentityClient()
                    {
                        IdentityClientId = "IAC_Cloud",
                        Description = "EPS",
                        SecretKey = "1a82f1d60ba6353bb64a8fb4b05e4bc4",
                        ClientType = 0,
                        IsActive = true,
                        RefreshTokenLifetime = 30,
                        AllowedOrigin = "*"
                    });

                    context.SaveChanges();

                    var passwordHasher = new PasswordHasher<User>();
                    var adminUser = new User
                    {
                        FullName = "Administrator",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "admin@gmail.com",
                        UserName = "admin",
                        NormalizedUserName = "admin",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        CompanyId = 1,
                        IsDelete = false,
                    };

                    adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Ab@123456");

                    context.Users.Add(adminUser);

                    context.SaveChanges();

                    adminUser.UserRoles.Add(new UserRole() { RoleId = 1, UserId = 1 });

                    context.SaveChanges();

                    var adminUserPrivileges = privileges.Select(x => new UserPrivilege() { UserId = 1, PrivilegeId = x.Id });
                    context.UserPrivileges.AddRange(adminUserPrivileges);

                    context.SaveChanges();

                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                    var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
                    Seed(userManager, roleManager);
                }
                catch (Exception ex)
                {

                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            IdentityResult result = null;
            List<User> users = new List<User>();
            DateTime dateTime = DateTime.UtcNow;
            for (int i = 10001; i <= 65000; i++)
            {
                users.Add(
                        new User()
                        {
                            UserName = $"student{i}@gmail.com",
                            NormalizedUserName = userManager.NormalizeName($"student{i}@gmail.com"),
                            Email = $"student{i}@gmail.com",
                            NormalizedEmail = userManager.NormalizeEmail($"student{i}@gmail.com"),
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = false,
                            Firstname = "Student",
                            Lastname = $"{i}",
                            SecurityStamp = Guid.NewGuid().ToString(),
                            Createdate = dateTime,
                            Updatedate = dateTime,
                            RegisterDate = dateTime,
                            BirthDate = null,
                            Creator = "Anonymous",
                            Updator = "Anonymous",
                            LockoutEnabled = true,
                            IsActive = true
                        }
                    ) ;
            }

            users.ForEach(user =>
            {
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, "P@$$w0rd");
                userManager.UpdateSecurityStampAsync(user);
                QueryHelper.CreateQuery(user);
            });
        }
    }
}

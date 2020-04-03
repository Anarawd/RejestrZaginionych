using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zaginionki.Models;
using Microsoft.AspNetCore.Identity;

namespace Zaginionki.Data
{
    public static class SeedData
    {

        public static void Initialize (IServiceProvider serviceProvider)
        {
            using (var context = new ZaginionkiContext(
                serviceProvider.GetRequiredService<DbContextOptions<ZaginionkiContext>>()))
            {
                if (context.Zaginiony.Any())
                {
                    return;
                }
                context.Zaginiony.AddRange(
                    new Zaginiony
                    {
                        Imie = "Benzen",
                        Nazwisko = "Toluen",
                        DataZaginiecia = DateTime.Parse("1989-01-01"),
                        Wojewodztwo = "Mazowieckie",
                        Opis = "aaaaaaaaaaa",
                        Plec = "Kobieta",
                        Zdjecie = "https://pbs.twimg.com/media/EUqkEynUUAQmmqO?format=jpg&name=small"
                    }
                    );
                context.SaveChanges();

            }
        }
        public static void seed(ZaginionkiContext context,UserManager<User> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            if(!context.Users.Any())
            {
                CreateUser(context, usermanager, rolemanager).GetAwaiter().GetResult();
            }
        }
        private static async Task CreateUser(ZaginionkiContext context, UserManager<User> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            string AdminRole = "Admin";
            string UserRole = "User";
            string VerifiedRole = "Verified";
            if (!await rolemanager.RoleExistsAsync(AdminRole))
            {
                await rolemanager.CreateAsync(new IdentityRole(AdminRole));
            }
            if (!await rolemanager.RoleExistsAsync(UserRole))
            {
                await rolemanager.CreateAsync(new IdentityRole(UserRole));
            }
            if (!await rolemanager.RoleExistsAsync(VerifiedRole))
            {
                await rolemanager.CreateAsync(new IdentityRole(VerifiedRole));
            }
            var UserAdmin = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "benzen@gmail.com"
            };
            if(await usermanager.FindByNameAsync(UserAdmin.UserName)==null)
            {
                await usermanager.CreateAsync(UserAdmin, "BenzenToluen");
                await usermanager.AddToRoleAsync(UserAdmin, AdminRole);
                await usermanager.AddToRoleAsync(UserAdmin, UserRole);
                UserAdmin.EmailConfirmed = true;
                UserAdmin.LockoutEnabled = false;
            }
            var UserUser = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "User",
                Email = "toluen@gmail.com"
            };
            if (await usermanager.FindByNameAsync(UserUser.UserName) == null)
            {
                await usermanager.CreateAsync(UserUser, "BenzenToluen");
                await usermanager.AddToRoleAsync(UserUser, UserRole);
                UserUser.EmailConfirmed = true;
                UserUser.LockoutEnabled = false;
            }
            var UserVerified = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "UserVer",
                Email = "hapapap@gmail.com"
            };
            if (await usermanager.FindByNameAsync(UserVerified.UserName) == null)
            {
                await usermanager.CreateAsync(UserVerified, "BenzenToluen");
                await usermanager.AddToRoleAsync(UserVerified, VerifiedRole);
                UserUser.EmailConfirmed = true;
                UserUser.LockoutEnabled = false;
            }
        }
    }
}

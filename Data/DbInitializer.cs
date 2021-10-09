using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamsIdentity.Models;

namespace TeamsIdentity.Data
{
    public class DbInitializer
    {
        public static async Task<Boolean> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            //Should I seed
            if (roleManager.Roles.Any())
                return false;

            Boolean result = await SeedRoles(roleManager);
            if (!result)
                return false;

            //Should I seed
            if (userManager.Users.Any())
                return false;

            return  await SeedUsers(userManager, configuration);
        }

        private static async Task<Boolean> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var result = await roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
                return false;
            
            result = await roleManager.CreateAsync(new IdentityRole("Player"));
            
            return result.Succeeded;
        }

        private static async Task<Boolean> SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            // Create Manager
            var manager = new ApplicationUser
            {
                UserName = "TheBoss",
                Email = "manager@team.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                BirthDate = DateTime.Now,
                PhoneNumber = "999-999-9999"
            };

            var result = await userManager.CreateAsync(manager, configuration["UserPassword"]);
            if (!result.Succeeded)
                return false;

            result = await userManager.AddToRoleAsync(manager, "Manager");
            if (!result.Succeeded)
                return false;

            // Create Player
            var player = new ApplicationUser
            {
                UserName = "Hitter",
                Email = "player@team.com",
                FirstName = "John",
                LastName = "Johnson",
                EmailConfirmed = true,
                BirthDate = DateTime.Now,
                PhoneNumber = "999-999-9999"
            };
            result = await userManager.CreateAsync(player, configuration["UserPassword"]);
            if (!result.Succeeded)
                return false;
            
            result = await userManager.AddToRoleAsync(player, "Player");
            
            return result.Succeeded;
        }
    }
}
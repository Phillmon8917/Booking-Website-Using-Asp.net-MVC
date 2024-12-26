namespace BookSession.Services
{
    using BookSession.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Identity;
    public class DatabaseInitializer
    {
        public static async Task SendDateAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (userManager == null || roleManager == null)
            {
                Console.WriteLine("UserManager or Role Manager is null");
                return;
            }

            //Create admin role if it does not exist
            var exists = await roleManager.RoleExistsAsync("admin");
            if (!exists)
            {
                Console.WriteLine("Admin role is not created!");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            //Create client role if it does not exist
            exists = await roleManager.RoleExistsAsync("client");
            if (!exists)
            {
                Console.WriteLine("Client role doesn't exist");
                await roleManager.CreateAsync(new IdentityRole("client"));
            }

            //checking if we have atleast one admin user
            var adminUser = await userManager.GetUsersInRoleAsync("admin");
            if (adminUser.Any())
            {
                Console.WriteLine("Admin user already exists => exit");
                return;
            }

            //Create the admin user
            var user = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                CreatedDate = DateTime.Now,
            };

            string initialPassword = "test@123";
            var results = await userManager.CreateAsync (user, initialPassword);
            if (results.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
                Console.WriteLine("Admin user created successfully! Please update the initial passwoerd!");
                Console.WriteLine("Email is: " + user.Email);
                Console.WriteLine("Passweord: " + initialPassword);
            }

            
        }
    }
}

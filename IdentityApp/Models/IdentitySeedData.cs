using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    public static class IdentitySeedData
    {
        public const string adminUser = "AdminUser";
        public const string adminPassword = "Admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app
                .ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<IdentityContext>();

            // Apply any pending migrations
            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app
                .ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = adminUser,
                    FullName = "Admin User",
                    Email = "admin@example.com",
                };

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}

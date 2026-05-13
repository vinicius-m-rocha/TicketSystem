using Microsoft.AspNetCore.Identity;

namespace TicketSystem.Services.Identity.Infrastructure.Data;

public static class SeedData
{
    public static async void EnsureSeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminUser = await userManager.FindByEmailAsync("admin@ticketsystem.com");
            if (adminUser == null)
            {
                var user = new IdentityUser 
                { 
                    UserName = "admin", 
                    Email = "admin@ticketsystem.com", 
                    EmailConfirmed = true 
                };
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
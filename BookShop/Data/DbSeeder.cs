using Microsoft.AspNetCore.Identity;
using BookShop.Domain.Constatns;
namespace BookShop.Ui.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //create admin user
            var admin = new IdentityUser
            {
                UserName = "stygmatyczny@gmail.com",
                Email = "stygmatyczny@gmail.com",
                EmailConfirmed = true
            };

            var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
            if(isUserExists is null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin,Roles.Admin.ToString());
            }

        }
    }
}

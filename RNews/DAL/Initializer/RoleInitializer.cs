using Microsoft.AspNetCore.Identity;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL.Initializer
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager,
                                                 RoleManager<IdentityRole> roleManager,
                                                 ApplicationDbContext db)
        {
            string adminEmail = "admin@rnews.com";
            string writerEmail = "writer@rnews.com";
            string readerEmail = "reader@rnews.com";
            string password = "3@Up-4@D";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("writer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("writer"));
            }
            if (await roleManager.FindByNameAsync("reader") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("reader"));
            }



            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult resultAdmin = await userManager.CreateAsync(admin, password);
                if (resultAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            if (await userManager.FindByNameAsync(writerEmail) == null)
            {
                User writer = new User { Email = writerEmail, UserName = writerEmail, EmailConfirmed = true };
                IdentityResult resultWriter = await userManager.CreateAsync(writer, password);
                if (resultWriter.Succeeded)
                {
                    await userManager.AddToRoleAsync(writer, "writer");
                }
            }
            if (await userManager.FindByNameAsync(readerEmail) == null)
            {
                User reader = new User { Email = readerEmail, UserName = readerEmail, EmailConfirmed = true };
                IdentityResult resultReader = await userManager.CreateAsync(reader, password);
                if (resultReader.Succeeded)
                {
                    await userManager.AddToRoleAsync(reader, "reader");
                }
            }


            if (db.Categories.Any())
            {
                return;
            }
            else
            {
                db.Categories.Add(new Category { Name = "Java" });
                db.Categories.Add(new Category { Name = "HTML/CSS" });
                db.Categories.Add(new Category { Name = "C#/.NET" });
                db.Categories.Add(new Category { Name = "UI/UX" });
                await db.SaveChangesAsync();
            }
            if (db.Tags.Any())
            {
                return;
            }
            else
            {
                db.Tags.Add(new Tag { TagName = "test"});
                await db.SaveChangesAsync();
            }
        }
    }
}

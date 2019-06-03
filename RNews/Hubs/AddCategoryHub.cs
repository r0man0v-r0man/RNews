using Microsoft.AspNetCore.SignalR;
using RNews.DAL;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    public class AddCategoryHub : Hub
    {
        private readonly ApplicationDbContext db;
        public AddCategoryHub(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddCategory(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var newCategory = new Category
                {
                    Name = name
                };
                await db.Categories.AddAsync(newCategory);
                await db.SaveChangesAsync();
                await Clients.All.SendAsync("RecieveCategory", newCategory.CategoryId, newCategory.Name);
            }
           
        }
    }
}


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Units
{
    public static class Unit
    {
        public static async Task<string> UploadPostMainImageAndGetPathAsync(IFormFile file, IHostingEnvironment appEnvironment)
        {
            
            if (file != null)
            {
                string path = "/imgs/posts/" + file.FileName;
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return path;
            }
            else
            {
                return "test";
            }
            
        }
    }
}

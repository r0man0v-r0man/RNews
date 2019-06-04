using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RNews.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class PostCreateViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public IFormFile Image { get; set; }
        public string Tags { get; set; }
    }
}

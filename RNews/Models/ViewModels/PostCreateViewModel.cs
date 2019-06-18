using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RNews.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class PostCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Tags { get; set; }
    }
}

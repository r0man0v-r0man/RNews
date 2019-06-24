using RNews.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class PropertyViewModel
    {
        public string PropertyViewModelId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public string Description { get; set; }
        public IList<string> Roles { get; set; }
        public List<Post> UserPosts { get; set; }
        public List<Comment> UserComments { get; set; }
    }
}

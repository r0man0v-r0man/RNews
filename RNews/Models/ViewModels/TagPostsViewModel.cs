using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class TagPostsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Rating { get; set; }
        public int CountOfComments { get; set; }
        public string PostImage { get; set; }
        public string Category { get; set; }
        public string UserImagePath { get; set; }
        public string Created { get; set; }
    }
}

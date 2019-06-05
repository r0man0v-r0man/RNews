using RNews.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class PostShowViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatar { get; set; }
        public DateTime DateOfCreatedAuthor { get; set; }
        public List<Comment> PostComments { get; set; }
        public List<PostTag> Tags { get; set; }
        public int Rating { get; set; }
    }
}

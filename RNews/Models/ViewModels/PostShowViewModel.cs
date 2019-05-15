using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class PostShowViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatar { get; set; }
        public DateTime DateOfCreatedAuthor { get; set; }

    }
}

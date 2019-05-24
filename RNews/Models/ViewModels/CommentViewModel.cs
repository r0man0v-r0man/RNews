using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }

    }
}

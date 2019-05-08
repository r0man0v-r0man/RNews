using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}

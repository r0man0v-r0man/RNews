using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public class CommentLike
    {
        public int CommentLikeId { get; set; }
        public bool? IsLike { get; set; } 
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}

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
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int LikesCount { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }

        public Comment()
        {
            Created = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int? Rating { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public Post()
        {
            User = new User();
            Created = DateTime.Now;
        }
    }
}

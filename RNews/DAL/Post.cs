using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public class Post
    {
        public int PostId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public User User { get; set; }
        public Post()
        {
            Created = DateTime.Now;
        }
    }
}

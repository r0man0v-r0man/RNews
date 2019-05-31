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
        public int Rating { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; } 

        public Post()
        {
            Created = DateTime.Now;
            PostTags = new List<PostTag>();
        }
    }
}

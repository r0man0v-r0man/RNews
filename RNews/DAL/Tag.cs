using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int TagCount { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }// = new List<PostTag>();
        public Tag()
        {
            PostTags = new List<PostTag>();
        }
    }
}

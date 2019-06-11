using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int Value { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}

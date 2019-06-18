using RNews.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class TagViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public List<Post> Posts { get; set; }
        public TagViewModel()
        {
            Posts = new List<Post>();
        }
    }
}

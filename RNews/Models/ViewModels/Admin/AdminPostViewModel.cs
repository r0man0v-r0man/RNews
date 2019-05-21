using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels.Admin
{
    public class AdminPostViewModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Rating { get; set; }
        public string DateCreated { get; set; }

    }
}

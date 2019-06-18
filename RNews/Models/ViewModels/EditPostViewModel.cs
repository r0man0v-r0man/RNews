using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
    }
}

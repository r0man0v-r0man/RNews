using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<string> Posts { get; set; }
        public ProfileViewModel()
        {
            Posts = new List<string>();
        }
    }
}

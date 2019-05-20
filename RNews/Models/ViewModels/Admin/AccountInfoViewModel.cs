using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Models.ViewModels.Admin
{
    public class AccountInfoViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> Roles { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string RegisterDate { get; set; }
        public string IsExternal { get; set; }
    }
}

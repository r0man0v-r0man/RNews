using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public class User : IdentityUser
    {
        public Gender Gender { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
    public enum Gender
    {
        male,
        female
    }
}

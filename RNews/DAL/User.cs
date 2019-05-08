using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RNews.DAL
{
    public class User : IdentityUser
    {
        public Gender Gender { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
    public enum Gender
    {
        male,
        female
    }
}

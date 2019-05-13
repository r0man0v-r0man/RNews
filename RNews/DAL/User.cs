using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RNews.DAL
{
    public class User : IdentityUser
    {
        public User()
        {
            ImagePath = "/imgs/avatars/user.png";
        }
        public Gender Gender { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
    public enum Gender
    {
        male,
        female
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace RNews.DAL
{
    public class User : IdentityUser
    {
        public User()
        {
            ImagePath = "/imgs/avatars/user.png";
            Created = DateTime.Now;
            IsExternal = false;
            Description = "I love RNews";
        }
        public virtual ICollection<Comment> Comments { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public string ImagePath { get; set; }
        public DateTime Created { get; set; }
        public string ImageName { get; set; }
        public bool IsExternal { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
    }
    public enum Gender
    {
        male,
        female
    }
}

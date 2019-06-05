using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RNews.DAL.EntityConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {

            builder
               .Property(c => c.Title)
                   .IsRequired()
                   .HasMaxLength(255);
            builder
                .Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(2000);
            builder
                .HasOne(c => c.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(c => c.UserId)
                .IsRequired();
            builder
                .HasMany(c => c.Ratings)
                .WithOne(p => p.Post);
            builder
                .HasOne(c => c.Category)
                .WithMany(p => p.Posts)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();
            builder
                .HasMany(c => c.Comments)
                .WithOne(p => p.Post);
        }
    }
}

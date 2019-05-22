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
                .Property(c => c.Category)
                .IsRequired();
            builder
                .HasMany(c => c.Comments)
                .WithOne(p => p.Post);
        }
    }
}

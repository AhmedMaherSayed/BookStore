using BookStore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Repository.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper()},
                new IdentityRole { Name = "Customer", NormalizedName = "Customer".ToUpper()}
                );
        }
    }
}

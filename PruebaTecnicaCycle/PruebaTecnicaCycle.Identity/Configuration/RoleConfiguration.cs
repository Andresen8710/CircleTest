using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PruebaTecnicaCycle.Identity.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "13F33DA2-59D8-4BB8-B0CD-92F4F338B323",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "0DEA77B0-5123-43AE-BD98-297A3CE3C7B2",
                    Name = "Seller",
                    NormalizedName = "SELLER"
                }
           );
        }
    }
}
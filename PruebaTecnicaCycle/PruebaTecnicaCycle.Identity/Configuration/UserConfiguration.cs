using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaCycle.Identity.Models;

namespace PruebaTecnicaCycle.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        //creo la data para la BD usando las variable de identityUser
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "E38B94A4-7974-45E7-9EE8-EAC63044F51D",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "admin@localhost.com",
                        Name = "Felipe",
                        LastName = "Bertrand",
                        UserName = "felbertr",
                        NormalizedUserName = "felbertr",
                        PasswordHash = hasher.HashPassword(null, "F1lipe2023*"),
                        EmailConfirmed = true,
                    },
                    new ApplicationUser
                    {
                        Id = "BF7A7C59-EDBE-49ED-8D4C-1744044EEC4E",
                        Email = "Laura@localhost.com",
                        NormalizedEmail = "Laura@localhost.com",
                        Name = "Laura",
                        LastName = "Castaño",
                        UserName = "laucast",
                        NormalizedUserName = "laucast",
                        PasswordHash = hasher.HashPassword(null, "L4ura2023*"),
                        EmailConfirmed = true,
                    }
                );
        }
    }
}
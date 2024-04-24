using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCycle.Identity.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(

                new IdentityUserRole<string>
                {
                    RoleId = "13F33DA2-59D8-4BB8-B0CD-92F4F338B323",
                    UserId = "E38B94A4-7974-45E7-9EE8-EAC63044F51D"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "0DEA77B0-5123-43AE-BD98-297A3CE3C7B2",
                     UserId = "BF7A7C59-EDBE-49ED-8D4C-1744044EEC4E"
                 }
            );
        }
    }
}

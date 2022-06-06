using ikifikir.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<user>
    {
        public void Configure(EntityTypeBuilder<user> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(70);
            builder.Property(x => x.LastName).HasMaxLength(70);
            builder.Property(x => x.Password).HasMaxLength(15);
            builder.Property(x => x.UserName).HasMaxLength(30);
            builder.Property(x => x.Email).HasMaxLength(70);
        }
    }
}

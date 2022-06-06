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
    public class TeamsMapping : IEntityTypeConfiguration<teams>
    {
        public void Configure(EntityTypeBuilder<teams> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.gmail).HasMaxLength(100);
            builder.Property(x => x.facebook).HasMaxLength(100);
            builder.Property(x => x.twitter).HasMaxLength(100);
            builder.Property(x => x.instagram).HasMaxLength(100);
            builder.Property(x => x.image).HasMaxLength(50);
            builder.Property(x => x.name).HasMaxLength(100);
            builder.Property(x => x.role).HasMaxLength(50);
        }
    }
}

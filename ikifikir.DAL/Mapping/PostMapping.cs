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
    public class PostMapping : IEntityTypeConfiguration<post>
    {
        public void Configure(EntityTypeBuilder<post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.title).HasMaxLength(70);
            builder.Property(x => x.spot).HasMaxLength(130);
            builder.Property(x => x.content).HasMaxLength(int.MaxValue);
            builder.Property(x => x.image).HasMaxLength(100);
            builder.Property(x => x.keywords).HasMaxLength(int.MaxValue);
            builder.Property(x => x.seoSpot).HasMaxLength(130);
            builder.Property(x => x.seoTitle).HasMaxLength(70);
            builder.Property(x => x.author).HasMaxLength(30);
        }
    }
}

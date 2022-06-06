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
    public class CategoryMapping : IEntityTypeConfiguration<category>
    {
        public void Configure(EntityTypeBuilder<category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.name).HasMaxLength(50);
            builder.Property(x => x.filterType).HasMaxLength(20);

        }
    }
}

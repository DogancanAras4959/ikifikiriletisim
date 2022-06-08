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
    public class ReferenceLogsMapping : IEntityTypeConfiguration<referenceLogo>
    {
        public void Configure(EntityTypeBuilder<referenceLogo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.slug).HasMaxLength(75);
        }
    }
}

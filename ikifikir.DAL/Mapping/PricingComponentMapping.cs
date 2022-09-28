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
    public class PricingComponentMapping : IEntityTypeConfiguration<pricingComponents>
    {
        public void Configure(EntityTypeBuilder<pricingComponents> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ComponentTitle).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.pricing).WithMany(x => x.pricingComponentsList).HasForeignKey(x => x.PricingId);
        }
    }
}

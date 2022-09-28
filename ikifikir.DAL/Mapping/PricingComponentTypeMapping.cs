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
    public class PricingComponentTypeMapping : IEntityTypeConfiguration<pricingComponentTypes>
    {
        public void Configure(EntityTypeBuilder<pricingComponentTypes> builder)
        {
            builder.Property(x => x.Id);
            builder.Property(x => x.Type).IsRequired().HasMaxLength(80);
            builder.HasOne(x => x.pricingComponents).WithMany(x => x.pricingComponentTypeList).HasForeignKey(x => x.pricingComponentId);
        }
    }
}

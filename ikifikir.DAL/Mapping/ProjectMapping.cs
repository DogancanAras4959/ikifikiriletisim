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
    public class ProjectMapping : IEntityTypeConfiguration<project>
    {
        public void Configure(EntityTypeBuilder<project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.client).HasMaxLength(50);
            builder.Property(x => x.description).HasMaxLength(5000);
            builder.Property(x => x.imageThumbnail).HasMaxLength(100);
            builder.Property(x => x.projectName).HasMaxLength(150);
            builder.Property(x => x.seoTitle).HasMaxLength(130);
            builder.Property(x => x.seoDescription).HasMaxLength(350);
            builder.Property(x => x.projectSpot).HasMaxLength(350);
            builder.Property(x => x.website).HasMaxLength(100);
            builder.HasOne(x => x.category).WithMany(x => x.projects).HasForeignKey(x => x.categoryId);
        }
    }
}

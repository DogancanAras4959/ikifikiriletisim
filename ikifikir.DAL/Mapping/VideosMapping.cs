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
    public class VideosMapping : IEntityTypeConfiguration<videos>
    {
        public void Configure(EntityTypeBuilder<videos> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.name).HasMaxLength(50);
            builder.Property(x => x.slug).HasMaxLength(70);
            builder.HasOne(x => x.projectVideos).WithMany(x => x.videoList).HasForeignKey(x => x.projectId);
        }
    }
}

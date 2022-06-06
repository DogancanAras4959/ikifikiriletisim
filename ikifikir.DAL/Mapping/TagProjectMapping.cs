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
    public class tagProjectMapping : IEntityTypeConfiguration<tagProject>
    {
        public void Configure(EntityTypeBuilder<tagProject> builder)
        {
            builder.HasOne(x => x.projectToTag).WithMany(x => x.tagProjectsProject).HasForeignKey(x => x.projectId);
            builder.HasOne(x => x.tagToTag).WithMany(x => x.tagProjects).HasForeignKey(x => x.tagId);
        }
    }
}

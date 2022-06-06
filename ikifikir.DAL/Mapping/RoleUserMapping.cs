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
    public class RoleUserMapping : IEntityTypeConfiguration<roleUsers>
    {
        public void Configure(EntityTypeBuilder<roleUsers> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.role).WithMany(x => x.roleUserList).HasForeignKey(x => x.roleId);
            builder.HasOne(x => x.user).WithMany(x => x.roleUserList).HasForeignKey(x => x.userId);
        }
    }
}

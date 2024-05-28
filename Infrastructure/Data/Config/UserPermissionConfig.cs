using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class UserPermissionConfig : IEntityTypeConfiguration<UserPermissions>
    {
        public void Configure(EntityTypeBuilder<UserPermissions> builder)
        {
           
            builder.ToTable("UserPermissions").HasKey(x => new { x.UserId, x.PermissionId });

        }
    }
}

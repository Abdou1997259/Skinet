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
    public class ProductBrandConfiguration:IEntityTypeConfiguration<ProdcutBrand>
    {
        public void Configure(EntityTypeBuilder<ProdcutBrand> builder)
        {

        }
    }
}

﻿using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Data.Config
{
    public class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=>x.Name) .IsRequired() .HasMaxLength(30);
            builder.HasData(
                new Category { Id = 1, Name = "test", Description = "test" }
                );
            //builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        }
    }
}

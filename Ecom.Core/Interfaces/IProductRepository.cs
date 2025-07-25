﻿using Ecom.Core.DTO_s;
using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository:IGenericRepositry<Product>
    {
        Task<bool> AddAsync(AddProductDTO productDTO);
    }
}

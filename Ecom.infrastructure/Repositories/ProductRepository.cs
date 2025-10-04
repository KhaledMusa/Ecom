using AutoMapper;
using Ecom.Core.DTO_s;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class ProductRepository : GenericRepositry<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManageService imageManageService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManageService imageManageService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManageService = imageManageService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null) return false;
            var product = mapper.Map<Product>(productDTO);
           await context.Products.AddAsync(product);
            await context.SaveChangesAsync();  
            var ImagePath = await imageManageService.AddImageAsync(productDTO.Photo, productDTO.Name);
            var photo = ImagePath.Select(path => new Photo
            {
    
                ImageName= path,
                ProductId = product.Id
            }).ToList();
           await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if(updateProductDTO is null) return false;
            var findProduct = await context.Products.Include(x=>x.Category)
                .Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.Id == updateProductDTO.Id);
            if (findProduct == null) return false;
            mapper.Map(updateProductDTO, findProduct);
            var photoNames = await context.Photos
                .Where(x => x.ProductId == updateProductDTO.Id).ToListAsync();
            foreach (var photo in photoNames)
            {
                imageManageService.DeleteImageAsync(photo.ImageName);
            }
            context.Photos.RemoveRange(photoNames);
            var ImagePath = await imageManageService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);
            var photos = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = findProduct.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

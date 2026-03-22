using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO_s;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWOrk work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllProducts([FromQuery] Ecom.Core.Specifications.ProductSpecParams productParams)
        {
            var spec = new Ecom.Core.Specifications.ProductWithCategorySpecification(productParams);
            var countSpec = new Ecom.Core.Specifications.ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await work.ProductRepository.CountAsync(countSpec);
            var products = await work.ProductRepository.GetAllWithSpecAsync(spec);

            var data = mapper.Map<IReadOnlyList<ProductDTO>>(products);

            return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }
        [HttpGet("get-by_ID/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var spec = new Ecom.Core.Specifications.ProductWithCategorySpecification(id);
            var product = await work.ProductRepository.GetEntityWithSpec(spec);
            
            if (product == null)
            {
                return NotFound(new ResponseAPI(404, "Product not found."));
            }

            var result = mapper.Map<ProductDTO>(product);
            return Ok(result);
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(AddProductDTO addproductDTO)
        {
            if (addproductDTO == null)
            {
                return BadRequest(new ResponseAPI(400, "Invalid product data."));
            }

            //var product = mapper.Map<Product>(addproductDTO);
            await work.ProductRepository.AddAsync(addproductDTO);
            return Ok();
        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct( UpdateProductDTO productDTO)
        {
            await  work.ProductRepository.UpdateAsync(productDTO);
            return Ok(new ResponseAPI(200, "Product updated successfully."));
        }
        [HttpDelete("Delete-Product/{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var product = await work.ProductRepository.GetByIdAsync(Id,x=>x.Photos, x => x.Category );
            if (product == null)
            {
                return NotFound(new ResponseAPI(404, "Product not found."));
            }
            await work.ProductRepository.DeleteAsync(product);
            return Ok(new ResponseAPI(200, "Product deleted successfully."));
        }
    }
}


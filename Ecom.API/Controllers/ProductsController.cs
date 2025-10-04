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
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await work.ProductRepository.GetAllAsync(x=>x.Category, x => x.Photos);
                var result = mapper.Map<List<ProductDTO>>(products);
                if (products == null )
                {
                    return BadRequest(new ResponseAPI(400, "No products found."));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by_ID/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id,x=>x.Category,X=>X.Photos);
                var result = mapper.Map<ProductDTO>(product);
                if (product == null)
                {
                    return NotFound(new ResponseAPI(404, "Product not found."));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, "No products found."));
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(AddProductDTO addproductDTO)
        {
            try
            {
                if (addproductDTO == null)
                {
                    return BadRequest(new ResponseAPI(400, "Invalid product data."));
                }

                //var product = mapper.Map<Product>(addproductDTO);
                await work.ProductRepository.AddAsync(addproductDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(500, ex.Message));
            }
        }
        [HttpPut("Update-Product/{id}")]
        public async Task<IActionResult> UpdateProduct( UpdateProductDTO productDTO)
        {
            try
            {
              await  work.ProductRepository.UpdateAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(500, ex.Message));
            }
        }
    }
}


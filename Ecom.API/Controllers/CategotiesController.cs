using Ecom.Core.DTO_s;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CategotiesController : BaseController
    {
        public CategotiesController(IUnitOfWOrk work) : base(work)
        {
        }
        [HttpGet("Get-all-Category")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await work.CategoryRepository.GetAllAsync();
                if (categories == null || !categories.Any())
                {
                    return BadRequest();
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("Get-Category-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add-Category")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
              
                var category = new Category()
                {
                    Name = categoryDTO.Name,
                    Description = categoryDTO.Description
                };
                await work.CategoryRepository.AddAsync(category);
                return Ok(new { message = "Item has been Added" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpPut("Update-Category")]
        public async Task<IActionResult> UpdateCategory(UpdatecategoryDTO updateCategoryDTO)
        {
            try
            {

                var category = new Category()
                {
                    Id = updateCategoryDTO.Id,
                    Name = updateCategoryDTO.Name,
                    Description = updateCategoryDTO.Description
                };
                await work.CategoryRepository.UpdateAsync(category);
                return Ok(new { message= "Item has been Updated" });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("Delete-Category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);
                return Ok(new { message = "Item has been Deleted" });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

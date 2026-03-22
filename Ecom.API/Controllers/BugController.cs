using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class BugController : BaseController
    {
        public BugController(IUnitOfWOrk work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var category = await work.CategoryRepository.GetByIdAsync(100);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var category = await work.CategoryRepository.GetByIdAsync(100);
            category.Name = "";
            return Ok(category);
        }
        [HttpGet("bad-request/{Id}")]
        public async Task<ActionResult> GetBadREquset(int Id)
        {
            return Ok();
        }
        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadREquset()
        {
            return BadRequest();
        }
    }
}

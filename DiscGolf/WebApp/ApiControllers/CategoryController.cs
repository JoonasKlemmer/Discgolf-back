
using System.ComponentModel;

using System.Net;

using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category> _mapper;


        public CategoryController(AppDbContext context, IAppBLL bll, UserManager<AppUser> userManager,  IMapper autoMapper)
        {
            _context = context;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category>(autoMapper);

        }

        // GET: api/Category
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Category>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Category>>> GetCategory()
        {
            var res = (await _bll.Categories.GetAllSortedAsync(
                    Guid.Parse(_userManager.GetUserId(User))
                ))
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);

        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        [ProducesResponseType<CategoryAttribute>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Category>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = category.Id
            }, category);
        }

        // DELETE: api/Category/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
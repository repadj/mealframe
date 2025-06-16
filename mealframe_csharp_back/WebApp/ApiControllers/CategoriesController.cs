using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DTO;
using App.DTO.v1;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IAppBLL _bll;
        private readonly App.DTO.v1.Mappers.CategoryMapper _mapper = new App.DTO.v1.Mappers.CategoryMapper();
        
        public CategoriesController(ILogger<CategoriesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Category>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return (await _bll.CategoryService.AllAsync()).Select(x => _mapper.Map(x)!).ToList();
        }

        /// <summary>
        /// Get single category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Category>> GetCategory(Guid id)
        {
            var contactType = await _bll.CategoryService.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return _mapper.Map(contactType)!;
        }

        /// <summary>
        /// Update category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            await _bll.CategoryService.UpdateAsync(_mapper.Map(category)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.Category), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Category>> PostCategory(App.DTO.v1.CategoryCreate category)
        {
            var bllCategory = _mapper.Map(category);
            _bll.CategoryService.Add(bllCategory);
            await _bll.SaveChangesAsync();

            var apiCategory = _mapper.Map(bllCategory);
            
            return CreatedAtAction(nameof(GetCategory), new { id = apiCategory!.Id }, apiCategory);
        }

        /// <summary>
        /// Delete category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _bll.CategoryService.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}

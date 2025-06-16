using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly ILogger<RecipeIngredientsController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.RecipeIngredientMapper _mapper = new App.DTO.v1.Mappers.RecipeIngredientMapper();

        public RecipeIngredientsController(ILogger<RecipeIngredientsController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all recipe ingredients
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.RecipeIngredient>>> GetRecipeIngredients()
        {
            return (await _bll.RecipeIngredientService.AllAsync()).Select(x => _mapper.Map(x)!).ToList();
        }

        // GET: api/RecipeIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.RecipeIngredient>> GetRecipeIngredient(Guid id)
        {
            var recipeIngredient = await _bll.RecipeIngredientService.FindAsync(id);

            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return _mapper.Map(recipeIngredient)!;
        }

        /// <summary>
        /// Update recipe ingredients
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recipeIngredient"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeIngredient(Guid id, App.DTO.v1.RecipeIngredient recipeIngredient)
        {
            if (id != recipeIngredient.Id)
            {
                return BadRequest();
            }

            await _bll.RecipeIngredientService.UpdateAsync(_mapper.Map(recipeIngredient)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new recipe ingredient
        /// </summary>
        /// <param name="recipeIngredient"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.RecipeIngredient>> PostRecipeIngredient(App.DTO.v1.RecipeIngredientCreate recipeIngredient)
        {
            var data = _mapper.Map(recipeIngredient)!;
            _bll.RecipeIngredientService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetRecipeIngredients", new { id = data.Id }, recipeIngredient);
        }

        /// <summary>
        /// Delete recipe ingredient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeIngredient(Guid id)
        {
            await _bll.RecipeIngredientService.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
    }
}

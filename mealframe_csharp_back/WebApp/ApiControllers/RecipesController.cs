using App.BLL.Contracts;
using App.DTO.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.RecipeMapper _mapper = new App.DTO.v1.Mappers.RecipeMapper();

        public RecipesController(ILogger<RecipesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all recipes for current user
        /// </summary>
        /// <returns>List of recipes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Recipe>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Recipe>>> GetRecipes()
        {
            var data = await _bll.RecipeService.AllAsync(User.GetUserId());
            var res = data.Select(x => _mapper.Map(x)!).ToList();
            return res;
        }

        /// <summary>
        /// Get recipe by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Recipe>> GetRecipe(Guid id)
        {
            var recipe = await _bll.RecipeService.FindAsync(id, User.GetUserId());

            if (recipe == null)
            {
                return NotFound();
            }

            return _mapper.Map(recipe)!;
        }

        /// <summary>
        /// Update recipe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutRecipe(Guid id, [FromBody] RecipeCreate recipe)
        {
            if (id == Guid.Empty) return BadRequest("Invalid ID");

            var bllRecipe = _mapper.Map(recipe)!;
            bllRecipe.Id = id;

            var updatedRecipe = await _bll.RecipeService.UpdateWithIngredientsAsync(bllRecipe, User.GetUserId());
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(updatedRecipe));
        }

        /// <summary>
        /// Create new recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.Recipe), StatusCodes.Status200OK)]
        public async Task<ActionResult<App.DTO.v1.Recipe>> PostRecipe([FromBody] App.DTO.v1.RecipeCreate recipe)
        {
            var bllRecipe = _mapper.Map(recipe);

            var savedRecipe = await _bll.RecipeService.CreateWithIngredientsAsync(bllRecipe, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            var returnDto = _mapper.Map(savedRecipe);

            return CreatedAtAction(nameof(GetRecipe), new { id = returnDto!.Id }, returnDto);
        }

        /// <summary>
        /// Delete recipe by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            await _bll.RecipeService.RemoveRecipeWithIngredientsAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        
        /// <summary>
        /// Get recipe with ingredients by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<RecipeDetail>> GetDetailedRecipe(Guid id)
        {
            var recipe = await _bll.RecipeService.GetDetailedAsync(id, User.GetUserId());

            if (recipe == null) return NotFound();

            return Ok(_mapper.MapDetailed(recipe));
        }
        
        /// <summary>
        /// Get recipes macros by recipe id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/macros")]
        [ProducesResponseType(typeof(App.DTO.v1.RecipeMacroSummary), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.RecipeMacroSummary>> GetRecipeMacros(Guid id)
        {
            var macros = await _bll.RecipeService.GetRecipeMacrosPerServingAsync(id, User.GetUserId());
            if (macros == null) return NotFound();

            var mapper = new App.DTO.v1.Mappers.RecipeMacroSummaryMapper();
            return Ok(mapper.Map(macros));
        }
    }
}

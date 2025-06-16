using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
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
    public class SavedRecipesController : ControllerBase
    {
        private readonly ILogger<SavedRecipesController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.SavedRecipeMapper _mapper = new App.DTO.v1.Mappers.SavedRecipeMapper();

        public SavedRecipesController(ILogger<SavedRecipesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all saved recipes for current user
        /// </summary>
        /// <returns>List of saved recipes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.SavedRecipe>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.SavedRecipe>>> GetSavedRecipes()
        {
            var data = await _bll.SavedRecipeService.AllAsync(User.GetUserId());
            var res = data.Select(x => _mapper.Map(x)!).ToList();
            return res;
        }

        /// <summary>
        /// Get recipe by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.SavedRecipe>> GetSavedRecipe(Guid id)
        {
            var savedRecipe = await _bll.SavedRecipeService.FindAsync(id, User.GetUserId());

            if (savedRecipe == null)
            {
                return NotFound();
            }

            return _mapper.Map(savedRecipe)!;
        }

        /// <summary>
        /// Update saved recipe by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="savedRecipe"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSavedRecipe(Guid id, App.DTO.v1.SavedRecipe savedRecipe)
        {
            if (id != savedRecipe.Id)
            {
                return BadRequest();
            }

            await _bll.SavedRecipeService.UpdateAsync(_mapper.Map(savedRecipe)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new saved recipe, will belong to the current user
        /// </summary>
        /// <param name="savedRecipe"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.SavedRecipe), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.SavedRecipe>> PostSavedRecipe(App.DTO.v1.SavedRecipeCreate savedRecipe)
        {
            var bllSavedRecipe = _mapper.Map(savedRecipe);
            _bll.SavedRecipeService.Add(bllSavedRecipe, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetSavedRecipes), new { id = bllSavedRecipe.Id }, savedRecipe);
        }

        /// <summary>
        /// Delete saved recipe by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSavedRecipe(Guid id)
        {
            await _bll.SavedRecipeService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        
    }
}

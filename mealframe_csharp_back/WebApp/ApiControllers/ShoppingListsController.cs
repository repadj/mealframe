using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShoppingListsController : ControllerBase
    {
        private readonly ILogger<ShoppingListsController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.ShoppingListMapper _mapper = new App.DTO.v1.Mappers.ShoppingListMapper();

        public ShoppingListsController(ILogger<ShoppingListsController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all shopping lists for current user
        /// </summary>
        /// <returns>List of shopping lists</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.ShoppingList>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ShoppingList>>> GetShoppingLists()
        {
            var data = await _bll.ShoppingListService.AllAsync(User.GetUserId());
            var res = data.Select(x => _mapper.Map(x)!).ToList();
            return res;
        }

        /// <summary>
        /// Get shopping list by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.ShoppingList>> GetShoppingList(Guid id)
        {
            var shoppingList = await _bll.ShoppingListService.FindAsync(id, User.GetUserId());

            if (shoppingList == null)
            {
                return NotFound();
            }

            return _mapper.Map(shoppingList)!;
        }

        /// <summary>
        /// Update shopping list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shoppingList"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingList(Guid id, App.DTO.v1.ShoppingList shoppingList)
        {
            if (id != shoppingList.Id)
            {
                return BadRequest();
            }

            await _bll.ShoppingListService.UpdateAsync(_mapper.Map(shoppingList)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new shopping list
        /// </summary>
        /// <param name="shoppingList"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.ShoppingList), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.ShoppingList>> PostShoppingList(App.DTO.v1.ShoppingListCreate shoppingList)
        {
            var bllShoppingList = _mapper.Map(shoppingList);
            _bll.ShoppingListService.Add(bllShoppingList, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetShoppingList), new { id = bllShoppingList.Id }, shoppingList);
        }

        /// <summary>
        /// Delete shopping list by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteShoppingList(Guid id)
        {
            await _bll.ShoppingListService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        
        /// <summary>
        /// Create a shopping list based on selected meal plans
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("generate")]
        public async Task<ActionResult<App.DTO.v1.ShoppingList>> GenerateShoppingList([FromBody] GenerateShoppingListRequest request)
        {
            var shoppingList = await _bll.ShoppingListService.GenerateFromMealPlansAsync(request.MealPlanIds, User.GetUserId());
            await _bll.SaveChangesAsync();

            return _mapper.Map(shoppingList)!;
        }
        
        /// <summary>
        /// Get detailed shopping list (with items, products, categories) for the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("detailed")]
        [ProducesResponseType(typeof(App.DTO.v1.ShoppingListDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.ShoppingListDetail>> GetDetailedShoppingList()
        {
            var shoppingList = await _bll.ShoppingListService.GetShoppingListWithItemsAsync(User.GetUserId());
            if (shoppingList == null) return NotFound();

            var mapped = _mapper.MapDetailed(shoppingList);
            return Ok(mapped);
        }
    }
}

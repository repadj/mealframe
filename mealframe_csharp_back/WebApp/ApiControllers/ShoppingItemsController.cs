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
    public class ShoppingItemsController : ControllerBase
    {
        private readonly ILogger<ShoppingItemsController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.ShoppingItemMapper _mapper = new App.DTO.v1.Mappers.ShoppingItemMapper();

        public ShoppingItemsController(ILogger<ShoppingItemsController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all shopping items
        /// </summary>
        /// <returns>list of shopping items</returns>
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ShoppingItem>>> GetShoppingItems()
        {
            return (await _bll.ShoppingItemService.AllAsync()).Select(x => _mapper.Map(x)!).ToList();
        }

        /// <summary>
        /// Get shopping item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.ShoppingItem>> GetShoppingItem(Guid id)
        {
            var shoppingItem = await _bll.ShoppingItemService.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            return _mapper.Map(shoppingItem)!;
        }

        /// <summary>
        /// Update a shopping item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shoppingItem"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingItem(Guid id, App.DTO.v1.ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }

            await _bll.ShoppingItemService.UpdateAsync(_mapper.Map(shoppingItem)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        /// <param name="shoppingItem"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.ShoppingItem>> PostShoppingItem(App.DTO.v1.ShoppingItemCreate shoppingItem)
        {
            var data = _mapper.Map(shoppingItem)!;
            _bll.ShoppingItemService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetShoppingItem", new { id = data.Id }, shoppingItem);
        }

        /// <summary>
        /// Delete shopping item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(Guid id)
        {
            await _bll.ShoppingItemService.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}

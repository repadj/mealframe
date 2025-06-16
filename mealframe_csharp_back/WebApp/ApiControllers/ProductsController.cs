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
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.ProductMapper _mapper = new App.DTO.v1.Mappers.ProductMapper();

        public ProductsController(ILogger<ProductsController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all products for current user
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Product>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Product>>> GetProducts()
        {
            var data = await _bll.ProductService.AllAsync(User.GetUserId());
            var res = data.Select(x => _mapper.Map(x)!).ToList();
            return res;
        }

        /// <summary>
        /// Get product by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Product>> GetProduct(Guid id)
        {
            var person = await _bll.ProductService.FindAsync(id, User.GetUserId());

            if (person == null)
            {
                return NotFound();
            }

            return _mapper.Map(person)!;
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, App.DTO.v1.Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _bll.ProductService.UpdateAsync(_mapper.Map(product)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Product>> PostProduct(App.DTO.v1.ProductCreate product)
        {
            var bllProduct = _mapper.Map(product);
            _bll.ProductService.Add(bllProduct, User.GetUserId());
            await _bll.SaveChangesAsync();
            var apiProduct = _mapper.Map(bllProduct)!;
            
            return CreatedAtAction(nameof(GetProduct), new { id = apiProduct.Id }, apiProduct);
        }

        /// <summary>
        /// Delete product by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _bll.ProductService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

//TODO - meal and USER relation via Person and MealType, might be broken

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class MealEntriesController : ControllerBase
    {
        private readonly ILogger<MealEntriesController> _logger;
        private readonly IAppBLL _bll;
        private readonly App.DTO.v1.Mappers.MealEntryMapper _mapper = new App.DTO.v1.Mappers.MealEntryMapper();

        /// <inheritdoc />
        public MealEntriesController(ILogger<MealEntriesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all meal entries
        /// </summary>
        /// <returns>List of meal entries</returns>
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.MealEntry>>> GetMealEntries()
        {
            var data = await _bll.MealEntryService.AllAsync();
            var res = data.Select(x => _mapper.Map(x)!).ToList();
            return res;
        }

        /// <summary>
        /// Get meal entry by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.MealEntry>> GetMealEntry(Guid id)
        {
            var mealEntry = await _bll.MealEntryService.FindAsync(id);

            if (mealEntry == null)
            {
                return NotFound();
            }

            return _mapper.Map(mealEntry)!;
        }

        /// <summary>
        /// Update meal entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mealEntry"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMealEntry(Guid id, App.DTO.v1.MealEntry mealEntry)
        {
            if (id != mealEntry.Id)
            {
                return BadRequest();
            }

            await _bll.MealEntryService.UpdateAsync(_mapper.Map(mealEntry)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new meal entry
        /// </summary>
        /// <param name="mealEntry"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.MealEntry>> PostMealEntry(App.DTO.v1.MealEntry mealEntry)
        {
            var data = _mapper.Map(mealEntry)!;
            _bll.MealEntryService.Add(data);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMealEntry", new { id = data.Id }, mealEntry);
        }

        /// <summary>
        /// Delete meal entry by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMealEntry(Guid id)
        {
            await _bll.MealEntryService.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
    }
}

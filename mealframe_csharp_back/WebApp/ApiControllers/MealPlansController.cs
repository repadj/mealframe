using App.BLL.Contracts;
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
    public class MealPlansController : ControllerBase
    {
        private readonly ILogger<MealPlansController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.MealPlanMapper _mapper = new App.DTO.v1.Mappers.MealPlanMapper();

        public MealPlansController(ILogger<MealPlansController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all mealplans
        /// </summary>
        /// <returns>List of meal plans</returns>
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.MealPlan>>> GetMealPlans()
        {
            return (await _bll.MealPlanService.AllAsync()).Select(x => _mapper.Map(x)!).ToList();
        }

        /// <summary>
        /// Get meal plan by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.MealPlan>> GetMealPlan(Guid id)
        {
            var mealPlan = await _bll.MealPlanService.FindAsync(id);

            if (mealPlan == null)
            {
                return NotFound();
            }

            return _mapper.Map(mealPlan)!;
        }

        /// <summary>
        /// Update meal plan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mealPlan"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMealPlan(Guid id, [FromBody] App.DTO.v1.MealPlanCreate mealPlan)
        {
            if (id == Guid.Empty) return BadRequest("Invalid ID");

            var bllMealPlan = _mapper.Map(mealPlan)!;
            bllMealPlan.Id = id;

            var updatedMealPlan = await _bll.MealPlanService.UpdateWithEntriesAsync(bllMealPlan, User.GetUserId());
            await _bll.SaveChangesAsync();

            return Ok(updatedMealPlan);
        }

        /// <summary>
        /// Create new mealPlan
        /// </summary>
        /// <param name="mealPlan"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.MealPlan>> PostMealPlan([FromBody] App.DTO.v1.MealPlanCreate mealPlan)
        {
            var userId = User.GetUserId();

            var bllMealPlan = _mapper.Map(mealPlan)!;
            var createdMealPlan = await _bll.MealPlanService.CreateWithEntriesAsync(bllMealPlan, userId);

            return CreatedAtAction(nameof(GetMealPlanDetail), new { id = createdMealPlan.Id }, _mapper.MapDetailed(createdMealPlan));

        }

        /// <summary>
        /// Delete mealPlan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMealPlan(Guid id)
        {
            await _bll.MealPlanService.RemoveWithEntriesAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        
        /// <summary>
        /// Get all mealPlans with entries (detailed)
        /// </summary>
        /// <returns>List of mealplans</returns>
        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.MealPlanDetail>>> GetAllMealPlansDetailed()
        {
            var userId = User.GetUserId();
            var bllMealPlans = await _bll.MealPlanService.GetAllWithEntriesAsync(userId);
            return Ok(bllMealPlans.Select(mp => _mapper.MapDetailed(mp)));
        }
        
        /// <summary>
        /// Get meal plan with entries by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<App.DTO.v1.MealPlanDetail>> GetMealPlanDetail(Guid id)
        {
            var bllMealPlan = await _bll.MealPlanService.GetMealPlanWithEntriesAsync(id, User.GetUserId());

            if (bllMealPlan == null) return NotFound();
            
            return Ok(_mapper.MapDetailed(bllMealPlan));
        }
        
        /// <summary>
        /// Get the macro summary for a meal plan (calculated macros from recipes & products)
        /// </summary>
        /// <param name="id">Meal Plan ID</param>
        /// <returns>Macro summary</returns>
        [HttpGet("{id}/macros")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.DTO.v1.MealPlanMacroSummary), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.DTO.v1.MealPlanMacroSummary>> GetMealPlanMacros(Guid id)
        {
            var macros = await _bll.MealPlanService.GetMealPlanMacrosAsync(id, User.GetUserId());

            if (macros == null)
            {
                return NotFound();
            }
            var mapper = new App.DTO.v1.Mappers.MealPlanMacroSummaryMapper();
            return Ok(mapper.Map(macros));
        }
    }
}

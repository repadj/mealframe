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
    public class NutritionalGoalsController : ControllerBase
    {
        private readonly ILogger<NutritionalGoalsController> _logger;
        private readonly IAppBLL _bll;

        private readonly App.DTO.v1.Mappers.NutritionalGoalMapper _mapper = new App.DTO.v1.Mappers.NutritionalGoalMapper();

        public NutritionalGoalsController(ILogger<NutritionalGoalsController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all nutritional goals for current user
        /// </summary>
        /// <returns>List of nutritional goals</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.NutritionalGoal>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.NutritionalGoal>>> GetNutritionalGoals()
        {
            var data = await _bll.NutritionalGoalService.AllAsync(User.GetUserId());
            var res = data.Select(x => _mapper.Map(x)!).ToList();
            return res;
        }

        /// <summary>
        /// Get nutritional goal by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.NutritionalGoal>> GetNutritionalGoal(Guid id)
        {
            var nutritionalGoal = await _bll.NutritionalGoalService.FindAsync(id, User.GetUserId());

            if (nutritionalGoal == null)
            {
                return NotFound();
            }

            return _mapper.Map(nutritionalGoal)!;
        }

        /// <summary>
        /// Update nutritional goal by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nutritionalGoal"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNutritionalGoal(Guid id, App.DTO.v1.NutritionalGoal nutritionalGoal)
        {
            if (id != nutritionalGoal.Id)
            {
                return BadRequest();
            }

            await _bll.NutritionalGoalService.UpdateAsync(_mapper.Map(nutritionalGoal)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new nutritional goal
        /// </summary>
        /// <param name="nutritionalGoal"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.NutritionalGoal>> PostNutritionalGoal(App.DTO.v1.NutritionalGoalCreate nutritionalGoal)
        {
            var bllNutritionalGoal = _mapper.Map(nutritionalGoal);
            _bll.NutritionalGoalService.Add(bllNutritionalGoal, User.GetUserId());
            await _bll.SaveChangesAsync();
            var apiGoal = _mapper.Map(bllNutritionalGoal)!;
            return CreatedAtAction(nameof(GetNutritionalGoals), new { id = apiGoal.Id }, apiGoal);
        }

        /// <summary>
        /// Delete nutritional goal by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionalGoal(Guid id)
        {
            await _bll.NutritionalGoalService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}

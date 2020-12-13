using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurveyApi.Models.Dtos;
using SurveyApi.Services;

namespace SurveyApi.Controllers
{
    /// <summary>
    ///     Survay Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]s")]
    public class SurveyController : ControllerBase
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly ISurveyService _service;

        /// <summary>
        ///     Survey Controller
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public SurveyController(ISurveyService service, ILogger<SurveyController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        ///     Get All Surveys
        /// </summary>
        /// <returns code="200" cerf="List&lt;SurveyDto&gt;">Success response</returns>
        /// <returns code="404">Fail to fetch survey or not found.</returns>
        /// <returns code="500">All other error.</returns>
        [HttpGet("GetAllSurveys")]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(List<SurveyDto>), StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(HttpResponse), 400)]
        public async Task<IActionResult> GetAllSurveys()
        {
            var result = await _service.GetAllSurveys();
            return result != null ? (IActionResult) Ok(result) : NotFound();
        }

        /// <summary>
        ///     Get Survey By Id
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns code="200" cref="SurveyDto">Success get modal by Id</returns>
        /// <returns code="404" cref="SurveyDto">Fail to fetch survey by provided id</returns>
        [HttpGet("GetSurveyById/{surveyId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status200OK)]
        // [ProducesResponseType(null, 404)]
        public async Task<IActionResult> GetSurveyById(Guid surveyId)
        {
            if (surveyId == Guid.Empty) return BadRequest("Invalid input surveyId.");

            var result = await _service.GetSurveyById(surveyId);
            return result != null ? (IActionResult) Ok(result) : NotFound();
        }

        /// <summary>
        ///     Create new survey.
        /// </summary>
        /// <remarks>
        ///     Input survey's options need to be structured.
        ///     system will automatically generate ids and retains the relationship.
        /// </remarks>
        /// <param name="newSurvey">No Ids need to provide. Ids will be assigned by system.</param>
        /// <returns code="200">Success created SurveyDto</returns>
        /// <returns code="400">Modal validation fail or creation fail.</returns>
        /// <returns code="500">All other internal error.</returns>
        [HttpPost("CreateSurvey")]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status200OK)]
        // [ProducesResponseType(null, 400)]
        public async Task<IActionResult> CreateSurvey([FromBody] SurveyDto newSurvey)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();
                _logger.LogError($"Invalid input. {errors}");
                return BadRequest($"Invalid input. {errors}");
            }

            var result = await _service.CreateSurvey(newSurvey);
            return result != null ? (IActionResult) Ok(result) : BadRequest("Fail to create survey.");
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurveyApi.Models.Dtos;
using SurveyApi.Services;

namespace SurveyApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]s")]
    public class AnswerController : ControllerBase
    {
        private readonly ILogger<AnswerController> _logger;
        private readonly ISurveyService _service;

        public AnswerController(ISurveyService service, ILogger<AnswerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpGet("GetAnswerById/{answerId}")]
        public async Task<IActionResult> GetAnswerById(Guid answerId)
        {
            if (answerId == Guid.Empty) return BadRequest("Invalid input answer id.");
            var result = await _service.GetAnswerById(answerId);
            return result != null ? (IActionResult) Ok(result) : NoContent();
        }

        /// <summary>
        ///     Create new Answer.
        /// </summary>
        /// <remarks>
        /// Input object should be hierarchy structure.
        /// Relationship Ids would be generated and handled properly.
        /// </remarks>
        /// <param name="newAnswer">No Ids need to provide. Ids will be assigned by system.</param>
        /// <returns code="200">Success created AnswerDto</returns>
        /// <returns code="400">Modal validation fail or creation fail.</returns>
        /// <returns code="500">All other internal error.</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(AnswerDto), StatusCodes.Status200OK)]
        [HttpPost("AddAnswer")]
        public async Task<IActionResult> AddAnswer([FromBody] AnswerDto newAnswer)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();
                _logger.LogError($"Invalid input. {errors}");
                return BadRequest($"Invalid input. {errors}");
            }

            var result = await _service.AddAnswer(newAnswer);
            return result != null ? (IActionResult) Ok(result) : NoContent();
        }
    }
}
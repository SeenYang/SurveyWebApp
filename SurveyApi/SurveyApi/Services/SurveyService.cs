using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SurveyApi.Models.Dtos;
using SurveyApi.Repository;

namespace SurveyApi.Services
{
    /// <summary>
    ///     This is the service that provide survey relevant functions.
    /// </summary>
    public class SurveyService : ISurveyService
    {
        private readonly ILogger<SurveyService> _logger;
        private readonly ISurveyRepository _repo;

        /// <summary>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repo"></param>
        public SurveyService(ILogger<SurveyService> logger, ISurveyRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        /// <summary>
        ///     Get All Surveys
        /// </summary>
        /// <returns></returns>
        public async Task<List<SurveyDto>> GetAllSurveys()
        {
            return await _repo.GetAllSurveys();
        }

        /// <summary>
        ///     Get Survey By Id
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public async Task<SurveyDto> GetSurveyById(Guid surveyId)
        {
            return await _repo.GetSurveyById(surveyId);
        }

        /// <summary>
        /// Create new Survey
        /// </summary>
        /// <remarks>
        /// Input survey should be structured. i.e. questions with respective options.
        /// </remarks>
        /// <param name="newSurvey">SurveyDto with structure options.</param>
        /// <returns></returns>
        public async Task<SurveyDto> CreateSurvey(SurveyDto newSurvey)
        {
            var existing = await _repo.GetSurveyByName(newSurvey.Name);
            if (existing == null || existing.Id == Guid.Empty)
            {
                var created = await _repo.CreateSurvey(newSurvey);
                return created;
            }

            _logger.LogError("Invalid new survey: Name exist.");
            return null;
        }

        /// <summary>
        /// Get Answer By AnserId
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public async Task<AnswerDto> GetAnswerById(Guid answerId)
        {
            var result = await _repo.GetAnswerById(answerId);

            return result;
        }

        /// <summary>
        /// Add new Answer
        /// </summary>
        /// <remarks>
        /// The questions answers should be structured in the input object.
        /// </remarks>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<AnswerDto> AddAnswer(AnswerDto answer)
        {
            var result = await _repo.AddAnswer(answer);

            return result;
        }
    }
}
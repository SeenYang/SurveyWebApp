using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SurveyApi.Models.Dtos;
using SurveyApi.Repository;

namespace SurveyApi.Services
{
    /// <summary>
    ///     This is the service that provide character relevant functions.
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
        ///     Get All Characters
        /// </summary>
        /// <returns></returns>
        public async Task<List<SurveyDto>> GetAllSurveys()
        {
            return await _repo.GetAllSurveys();
        }

        /// <summary>
        ///     Get Character By Id
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public async Task<SurveyDto> GetSurveyById(Guid characterId)
        {
            return await _repo.GetSurveyById(characterId);
        }

        /// <summary>
        ///     Input character should be structured options.
        ///     The hierarchy will be flatten and saved.
        /// </summary>
        /// <param name="newSurvey">CharacterDto with structure options.</param>
        /// <returns></returns>
        public async Task<SurveyDto> CreateSurvey(SurveyDto newSurvey)
        {
            // var existing = await _repo.GetSurveyByName(newSurvey.Name);
            // if (existing == null)
            // {
            //     var created = await _repo.CreateSurvey(newSurvey);
            //     if (newSurvey.Options != null && newSurvey.Options.Any())
            //     {
            //         var flatted = InitiateNewOptionList(newSurvey.Options, created.Id);
            //         var result = await _repo.CreateQuestions(flatted);
            //         if (!result)
            //         {
            //             _logger.LogError($"Fail to create options for character {created.Id}.");
            //             return null;
            //         }
            //     }
            //
            //     var returnResult = await _repo.GetSurveyById(created.Id);
            //     return returnResult;
            // }

            _logger.LogError("Invalid new character: Name exist.");
            return null;
        }

        public async Task<AnswerDto> GetAnswerById(Guid answerId)
        {
            throw new NotImplementedException();
        }

        public async Task<AnswerDto> AddAnswer(AnswerDto answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Get Options By CharacterId
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public async Task<List<QuestionDto>> GetQuestionsBySurveyId(Guid characterId)
        {
            return await _repo.GetQuestionsBySurveyId(characterId);
        }

        /// <summary>
        ///     Update Option
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        // public async Task<OptionDto> UpdateOption(OptionDto option)
        // {
        //     var character = await _repo.GetSurveyById(option., false);
        //     if (character == null)
        //     {
        //         _logger.LogError($"Fail to create new Option: Character {option.CharacterId} can not be found.");
        //         return null;
        //     }
        //
        //     return await _repo.UpdateQuestion(option);
        // }

        /// <summary>
        ///     Taking structured option list, initiate the ids.
        ///     * New Ids will be generated and assign. Don't need to provide ID.
        /// </summary>
        /// <param name="options">Structured Option list</param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        private List<OptionDto> InitiateNewOptionList(List<OptionDto> options, Guid characterId)
        {
            var returnList = new List<OptionDto>();
            // foreach (var option in options)
            // {
            //     var optionId = Guid.NewGuid();
            //     option.Id = optionId;
            //     option.CharacterId = characterId;
            //     returnList.Add(option);
            //     if (option.SubOptions.Any())
            //         returnList.AddRange(InitiateSubOptions(option.SubOptions, optionId, characterId));
            // }

            return returnList;
        }

        private List<OptionDto> InitiateSubOptions(List<OptionDto> subOptions, Guid parentId, Guid characterId)
        {
            var returnList = new List<OptionDto>();
            // foreach (var option in subOptions)
            // {
            //     var optionId = Guid.NewGuid();
            //     option.Id = optionId;
            //     option.ParentOptionId = parentId;
            //     option.CharacterId = characterId;
            //     if (option.SubOptions.Any())
            //         returnList.AddRange(InitiateSubOptions(option.SubOptions, optionId, characterId));
            // }
            //
            // returnList.AddRange(subOptions);
            return returnList;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApi.Models.Dtos;

namespace SurveyApi.Repository
{
    public interface ISurveyRepository
    {
        // Survey
        Task<List<SurveyDto>> GetAllSurveys();
        Task<SurveyDto> GetSurveyById(Guid surveyId, bool includeOption = true);
        /// <summary>
        /// This is the method for internal usage mainly.
        /// Provide name and see whether we have the same name already.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<SurveyDto> GetSurveyByName(string name);
        Task<SurveyDto> CreateSurvey(SurveyDto newSurvey);
        Task<SurveyDto> UpdateSurvey(SurveyDto survey);
        
        // Options
        Task<List<QuestionDto>> GetQuestionsBySurveyId(Guid surveyId);
        Task<bool> CreateQuestions(List<OptionDto> newOptions);
        Task<OptionDto> UpdateQuestion(OptionDto option);
    }
}
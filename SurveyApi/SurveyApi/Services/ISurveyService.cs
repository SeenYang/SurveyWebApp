using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApi.Models.Dtos;
using SurveyApi.Models.Entities;

namespace SurveyApi.Services
{
    public interface ISurveyService
    {
        // Survey
        Task<List<SurveyDto>> GetAllSurveys();
        Task<SurveyDto> GetSurveyById(Guid surveyId);
        Task<SurveyDto> CreateSurvey(SurveyDto newSurvey);

        // Answers
        Task<AnswerDto> GetAnswerById(Guid answerId);
        Task<AnswerDto> AddAnswer(AnswerDto answer);
    }
}
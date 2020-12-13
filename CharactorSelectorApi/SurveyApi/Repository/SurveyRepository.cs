using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurveyApi.Models;
using SurveyApi.Models.Dtos;
using SurveyApi.Models.Entities;

namespace SurveyApi.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly SurveyDbContext _context;
        private readonly ILogger<SurveyRepository> _logger;
        private readonly IMapper _map;

        public SurveyRepository(SurveyDbContext context, IMapper map, ILogger<SurveyRepository> logger)
        {
            _context = context;
            _map = map;
            _logger = logger;
        }

        /// <summary>
        ///     Get All Surveys
        ///     This method won't return surveys' question
        /// </summary>
        /// <returns></returns>
        public async Task<List<SurveyDto>> GetAllSurveys()
        {
            var entities = await _context.Surveys.ToListAsync();
            var result = _map.Map<List<Survey>, List<SurveyDto>>(entities);
            return result;
        }

        /// <summary>
        ///     Get Survey by Id.
        ///     This method will return whole structure of the Survey's options
        /// </summary>
        /// <returns></returns>
        public async Task<SurveyDto> GetSurveyById(Guid surveyId, bool includeOption = true)
        {
            var entity = await _context.Surveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(c => c.Id == surveyId);
            
            if (entity == null)
            {
                // handle null case in service level.
                return new SurveyDto();
            }
            
            var result = _map.Map<Survey, SurveyDto>(entity);
            return result;
        }

        /// <summary>
        ///     This method only for validation the Survey's name exist or not.
        ///     Need to be extended if another requirement come in.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Survey without options' hierarchy.</returns>
        public async Task<SurveyDto> GetSurveyByName(string name)
        {
            var entity = await _context.Surveys.FirstOrDefaultAsync(c => c.Name == name);
            if (entity == null)
            {
                // handle null case in service level.
                return new SurveyDto();
            }
            var result = _map.Map<Survey, SurveyDto>(entity);
            return result;
        }

        /// <summary>
        ///     Create Survey.
        ///     * Only Survey table, option table will be inserted by another method.
        ///     * Survey ID will be assigned by DB, and populated to options in caller.
        /// </summary>
        /// <param name="newSurvey"></param>
        /// <returns></returns>
        public async Task<SurveyDto> CreateSurvey(SurveyDto newSurvey)
        {
            try
            {
                var entity = _map.Map<SurveyDto, Survey>(newSurvey);
                var created = await _context.Surveys.AddAsync(entity);
                await _context.SaveChangesAsync();
                var result = _map.Map<Survey, SurveyDto>(created.Entity);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Fail to create new Survey. Exception: {e}");
                throw new Exception("Fail to create new Survey.");
            }
        }

        /// <summary>
        ///     Update Survey
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SurveyDto> UpdateSurvey(SurveyDto survey)
        {
            try
            {
                var entity = _map.Map<SurveyDto, Survey>(survey);
                // todo: Ask for lock release

                var created = _context.Surveys.Update(entity);
                await _context.SaveChangesAsync();
                var result = _map.Map<Survey, SurveyDto>(created.Entity);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Fail to update Survey {survey.Id}. Exception: {e}");
                throw new Exception("Fail to update Survey {Survey.Id}.");
            }
        }

        public async Task<List<QuestionDto>> GetQuestionsBySurveyId(Guid SurveyId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Create Option
        /// </summary>
        /// <param name="newOptions"></param>
        /// <returns></returns>
        public async Task<bool> CreateQuestions(List<OptionDto> newOptions)
        {
            try
            {
                // Due to the input is hierarchy structured, we want to keep the relationship.
                var entities = _map.Map<List<OptionDto>, List<Option>>(newOptions);
                // todo: Ask for lock release
                await _context.Options.AddRangeAsync(entities);
                _context.Options.FromSqlRaw("SET IDENTITY_INSERT dbo.Option ON");
                await _context.SaveChangesAsync();
                _context.Options.FromSqlRaw("SET IDENTITY_INSERT dbo.Option OFF");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError($"Fail to update new option. Exception: {e}");
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<OptionDto> UpdateQuestion(OptionDto option)
        {
            try
            {
                var entity = _map.Map<OptionDto, Option>(option);
                // todo: Ask for lock release
                var created = _context.Options.Update(entity);
                await _context.SaveChangesAsync();
                var result = _map.Map<Option, OptionDto>(created.Entity);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError($"Fail to update Option {option.Id}. Exception: {e}");
            }

            return null;
        }
    }
}
using AutoMapper;
using SurveyApi.Models.Dtos;
using SurveyApi.Models.Entities;

namespace SurveyApi.Models
{
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        ///     Add mapping config under this file.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Survey, SurveyDto>();
            CreateMap<SurveyDto, Survey>();
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();
            CreateMap<Option, OptionDto>();
            CreateMap<OptionDto, Option>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
using System;
using System.Threading.Tasks;
using SurveyApi.Models.Dtos;

namespace SurveyApi.Services
{
    public interface IUserService
    {
        public Task<UserDto> GetUserById(Guid id);
        public Task<UserDto> CreateUser(UserDto newUser);
    }
}
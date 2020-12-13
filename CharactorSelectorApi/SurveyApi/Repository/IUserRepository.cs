using System;
using System.Threading.Tasks;
using SurveyApi.Models.Dtos;

namespace SurveyApi.Repository
{
    public interface IUserRepository
    {
        public Task<UserDto> GetUserById(Guid id);
        public Task<UserDto> CreateUser(UserDto newUser);
    }
}
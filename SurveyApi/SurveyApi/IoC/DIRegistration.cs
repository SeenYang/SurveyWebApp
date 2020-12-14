using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApi.Repository;
using SurveyApi.Services;

namespace SurveyApi.IoC
{
    public static class DIRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurveyApi.Models.Dtos;
using SurveyApi.Models.Entities;

namespace SurveyApi.Models
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new SurveyDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<SurveyDbContext>>());

            if (context.Surveys.Any() && context.Options.Any() && context.Options.Any()) return;

            var survey1 = new Survey
            {
                Id = Guid.NewGuid(),
                Name = "Survey 1",
                CreatedDateUtc = DateTime.UtcNow,
                UpdatedDateUtc = DateTime.UtcNow,
                UserId = Guid.NewGuid(),
                UserName = "Fake User",
                Type = 1
            };

            var question1 = new Question
            {
                Id = Guid.NewGuid(),
                CreatedDateUtc = DateTime.UtcNow,
                UserId = survey1.UserId,
                UserName = survey1.UserName,
                Title = "How many astronauts landed on the moon?",
                QuestionType = 1,
                Order = 1,
            };

            var option1 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "1",
                QuestionId = question1.Id
            };
            var option2 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "3",
                QuestionId = question1.Id
            };
            var option3 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "18",
                QuestionId = question1.Id
            };

            question1.Options = new List<Option> {option1, option2, option3};

            var question2 = new Question
            {
                Id = Guid.NewGuid(),
                CreatedDateUtc = DateTime.UtcNow,
                UserId = survey1.UserId,
                UserName = survey1.UserName,
                Title = "How many devs does it take to change a lightbulb?",
                SubTitle = "This is not a trick question.",
                QuestionType = 1,
                Order = 2
            };

            var option4 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "One",
                QuestionId = question2.Id
            };
            var option5 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "Two",
                QuestionId = question2.Id
            };
            var option6 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "Three thousand three hundred eighty-seven",
                QuestionId = question2.Id
            };

            var survey2 = new Survey
            {
                Id = Guid.NewGuid(),
                Name = "Survey 2",
                CreatedDateUtc = DateTime.UtcNow,
                UpdatedDateUtc = DateTime.UtcNow,
                UserId = Guid.NewGuid(),
                UserName = "Fake User",
                Type = 1
            };

            question2.Options = new List<Option> {option4, option5, option6};
            survey1.Questions = new List<Question> {question1, question2};

            var question3 = new Question
            {
                Id = Guid.NewGuid(),
                CreatedDateUtc = DateTime.UtcNow,
                UserId = survey1.UserId,
                UserName = survey1.UserName,
                Title = "What is the temp today?",
                SubTitle = "We need to send this to the Bureau of Meteorology.",
                QuestionType = 1
            };

            var option7 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "10°",
                QuestionId = question1.Id
            };
            var option8 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "20°",
                QuestionId = question1.Id
            };
            var option9 = new Option
            {
                Id = Guid.NewGuid(),
                Text = "30°",
                QuestionId = question1.Id
            };
            question3.Options = new List<Option> {option7, option8, option9};
            survey2.Questions = new List<Question> {question3};

            context.Surveys.AddRange(survey1, survey2);
            context.Questions.AddRange(question1, question2, question3);
            context.Options.AddRange(
                option1,
                option2,
                option3,
                option4,
                option5,
                option6,
                option7,
                option8,
                option9
            );
            
            context.SaveChanges();
        }
    }
}
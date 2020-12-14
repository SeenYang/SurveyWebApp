using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using SurveyApi.Models.Dtos;
using SurveyApi.Repository;
using SurveyApi.Services;
using Xunit;

namespace SurveyApi.Tests
{
    public class SurveyServiceTests
    {
        private readonly Mock<ILogger<SurveyService>> _logger;
        private readonly Mock<ISurveyRepository> _repo;
        private readonly ISurveyService _service;

        public SurveyServiceTests()
        {
            _logger = new Mock<ILogger<SurveyService>>();
            _repo = new Mock<ISurveyRepository>();

            _service = new SurveyService(_logger.Object, _repo.Object);
        }

        [Fact(DisplayName = "Add new Survey, name exist, should return null, logger log error")]
        public async void Test1()
        {
            _repo.Setup(r => r.GetSurveyByName(It.IsAny<string>())).ReturnsAsync(new SurveyDto
            {
                Id = Guid.NewGuid()
            });

            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.CreateSurvey(new SurveyDto());
                Assert.Null(result);
            });

            Assert.Null(exception);
        }
        
        [Fact(DisplayName = "Add new Survey, name not exist, should create, return new object.")]
        public async void Test2()
        {
            // return empty object if no found.
            _repo.Setup(r => r.GetSurveyByName(It.IsAny<string>())).ReturnsAsync(new SurveyDto());
            var expectedId = Guid.NewGuid();
            _repo.Setup(r => r.CreateSurvey(It.IsAny<SurveyDto>())).ReturnsAsync((SurveyDto input) =>
            {
                input.Id = expectedId;
                input.Questions?.ForEach(q => q.SurveyId = expectedId);
                return input;
            });
            
            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.CreateSurvey(new SurveyDto
                {
                    Name = "New Survey",
                    Questions = new List<QuestionDto>
                    {
                        new QuestionDto
                        {
                            Title = "New Question"
                        }
                    }
                });
                Assert.NotNull(result);
                Assert.Equal(expectedId, result.Id);
                Assert.Equal(expectedId, result?.Questions[0].SurveyId);
                
            });

            Assert.Null(exception);
        }
    }
}
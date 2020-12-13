using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApi.Models.Dtos;
using SurveyApi.Repository;
using SurveyApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CharacterSelectorApi.Tests
{
    public class CharacterService_CreateCharacter_Tests
    {
        private readonly Mock<ILogger<SurveyService>> _logger;
        private readonly Mock<ISurveyRepository> _repo;
        private readonly ISurveyService _service;

        public CharacterService_CreateCharacter_Tests()
        {
            _repo = new Mock<ISurveyRepository>();
            _logger = new Mock<ILogger<SurveyService>>();
            _service = new SurveyService(_logger.Object, _repo.Object);
        }

        [Fact(DisplayName = "Create Character, name exist, should return null.")]
        public async Task Test1()
        {
            _repo.Setup(r => r.GetSurveyByName(It.IsAny<string>())).ReturnsAsync(new SurveyDto());

            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.CreateSurvey(new SurveyDto {Name = "Any Name"});

                Assert.Null(result);
            });

            Assert.Null(exception);
        }

        [Fact(DisplayName = "Create Character, name not exist, not options, should return new character.")]
        public async Task Test2()
        {
            _repo.Setup(r => r.GetSurveyByName(It.IsAny<string>())).Returns(Task.FromResult<SurveyDto>(null));
            _repo.Setup(r => r.CreateSurvey(It.IsAny<SurveyDto>()))
                .ReturnsAsync(new SurveyDto {Id = Guid.NewGuid()});
            var created = new SurveyDto {Id = Guid.NewGuid()};
            _repo.Setup(r => r.GetSurveyById(It.IsAny<Guid>(), true)).ReturnsAsync(created);
            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.CreateSurvey(created);

                Assert.NotNull(result);
                Assert.Equal(created.Id, result.Id);
            });

            Assert.Null(exception);
        }

        [Fact(DisplayName = "Create Character, name not exist, with options, should return new character.")]
        public async Task Test3()
        {
            _repo.Setup(r => r.GetSurveyByName(It.IsAny<string>())).Returns(Task.FromResult<SurveyDto>(null));
            _repo.Setup(r => r.CreateSurvey(It.IsAny<SurveyDto>()))
                .ReturnsAsync(new SurveyDto {Id = Guid.NewGuid()});
            var created = new SurveyDto
            {
                Id = Guid.NewGuid(),
                Options = new List<OptionDto> {new OptionDto {Name = "option1", SubOptions = new List<OptionDto>()}}
            };
            _repo.Setup(r => r.GetSurveyById(It.IsAny<Guid>(), true)).ReturnsAsync(created);
            _repo.Setup(r => r.CreateQuestions(It.IsAny<List<OptionDto>>())).ReturnsAsync(true);
            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.CreateSurvey(created);

                Assert.NotNull(result);
                Assert.Equal(created.Id, result.Id);
            });

            Assert.Null(exception);
        }

        [Fact(DisplayName = "Create Character, name not exist, with hierarchy options, should return new character.")]
        public async Task Test4()
        {
            var created = new SurveyDto
            {
                Id = Guid.NewGuid(),
                Options = new List<OptionDto>
                {
                    new OptionDto
                    {
                        Name = "option1",
                        SubOptions = new List<OptionDto>
                        {
                            new OptionDto
                            {
                                Name = "suboption",
                                SubOptions = new List<OptionDto>()
                            }
                        }
                    }
                }
            };
            var mockOptionContext = new List<OptionDto>();

            _repo.Setup(r => r.GetSurveyByName(It.IsAny<string>())).Returns(Task.FromResult<SurveyDto>(null));
            _repo.Setup(r => r.CreateSurvey(It.IsAny<SurveyDto>()))
                .ReturnsAsync(new SurveyDto {Id = Guid.NewGuid()});
            _repo.Setup(r => r.GetSurveyById(It.IsAny<Guid>(), true)).ReturnsAsync(created);
            _repo.Setup(r => r.CreateQuestions(It.IsAny<List<OptionDto>>())).Callback((List<OptionDto> input) =>
            {
                mockOptionContext.AddRange(input);
            }).ReturnsAsync(true);

            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _service.CreateSurvey(created);

                Assert.NotNull(result);
                Assert.Equal(created.Id, result.Id);
                Assert.Equal(2, mockOptionContext.Count);
            });

            Assert.Null(exception);
        }
    }
}
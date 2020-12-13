using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SurveyApi.Models;
using SurveyApi.Models.Entities;
using SurveyApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace CharacterSelectorApi.Tests
{
    public class CharacterRepository_GetCharacterById_Tests
    {
        private readonly Mock<ChracterSelectorContext> _context;
        private readonly Mock<ILogger<SurveyRepository>> _logger;
        private readonly IMapper _map;
        private readonly ISurveyRepository _repo;

        public CharacterRepository_GetCharacterById_Tests()
        {
            _context = new Mock<ChracterSelectorContext>(new DbContextOptions<ChracterSelectorContext>());
            _logger = new Mock<ILogger<SurveyRepository>>();
            var mapper = new MapperConfiguration(c => { c.AddProfile(typeof(AutoMapperProfile)); });
            _map = mapper.CreateMapper();
            _repo = new SurveyRepository(_context.Object, _map, _logger.Object);
        }

        [Fact(DisplayName = "Get Character by Id, not include options, return character only")]
        public async void Test1()
        {
            var characterId = Guid.NewGuid();
            var characterList = new List<Survey>
            {
                new Survey {Id = characterId, Name = "fake character"}
            }.AsQueryable().BuildMockDbSet();
            _context.Setup(m => m.Characters).Returns(characterList.Object);

            var optionList = new List<Option>
            {
                new Option
                {
                    Id = Guid.NewGuid(),
                    Text = "fake option",
                    CharacterId = characterId
                }
            }.AsQueryable().BuildMockDbSet();
            _context.Setup(m => m.Options).Returns(optionList.Object);

            var result = await _repo.GetSurveyById(characterId, false);

            Assert.NotNull(result);
            Assert.Equal(characterId, result.Id);
            Assert.Empty(result.Options);
        }

        [Fact(DisplayName = "Get Character by Id, include options, return character and options")]
        public async void Test2()
        {
            var characterId = Guid.NewGuid();
            var characterList = new List<Survey>
            {
                new Survey {Id = characterId, Name = "fake character"}
            }.AsQueryable().BuildMockDbSet();
            _context.Setup(m => m.Characters).Returns(characterList.Object);

            var optionList = new List<Option>
            {
                new Option
                {
                    Id = Guid.NewGuid(),
                    Text = "fake option",
                    CharacterId = characterId
                }
            }.AsQueryable().BuildMockDbSet();
            _context.Setup(m => m.Options).Returns(optionList.Object);

            var result = await _repo.GetSurveyById(characterId);

            Assert.NotNull(result);
            Assert.Equal(characterId, result.Id);
            Assert.NotEmpty(result.Options);
        }
    }
}
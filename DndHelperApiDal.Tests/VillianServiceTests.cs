using AutoFixture;
using DndHelperApiCore.Tests;
using DndHelperApiDal.Configurations;
using DndHelperApiDal.Models;
using DndHelperApiDal.Queries;
using DndHelperApiDal.Repositories;
using DndHelperApiDal.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DndHelperApiDal.Tests
{
    public class VillianServiceTests : TestBase
    {
        private readonly Mock<IRepository> _mockRepository;
        private readonly IOptions<ConnectionConfiguration> _configuration;

        public VillianServiceTests()
        {
            _mockRepository = new Mock<IRepository>();
            _configuration = Options.Create(new ConnectionConfiguration());
        }

        [Fact]
        public async Task Should_GetVillianObjectivesAsync()
        {
            var expected = Fixture.CreateMany<VillianObjective>();
            _mockRepository.Setup(x => x.QueryAsync<VillianObjective>(VillianQueries.GetVillianObjectives, CommandType.Text)).ReturnsAsync(expected);
            var villianService = new VillianService(_configuration, _mockRepository.Object);
            var result = await villianService.GetVillianObjectivesAsync();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Should_GetVillianObjectiveSchemesAsync()
        {
            var objectives = Fixture.CreateMany<string>(2).ToList();
            var schemes = Fixture.CreateMany<string>(3).ToList();

            var villianObjectiveSchemes = new List<VillianObjectiveScheme>
            {
                new VillianObjectiveScheme
                {
                    Id = 1,
                    Objective = objectives[0],
                    Scheme = schemes[0]
                },
                new VillianObjectiveScheme
                {
                    Id = 1,
                    Objective = objectives[0],
                    Scheme = schemes[1]
                },
                new VillianObjectiveScheme
                {
                    Id = 2,
                    Objective = objectives[1],
                    Scheme = schemes[2]
                }
            };

            var expected = new List<VillianObjectiveSchemes>
            {
                new VillianObjectiveSchemes
                {
                    Id = 1,
                    Objective = objectives[0],
                    Scheme = new List<string>
                    {
                        schemes[0],
                        schemes[1]
                    }
                },
                new VillianObjectiveSchemes
                {
                    Id = 2,
                    Objective = objectives[1],
                    Scheme = new List<string>
                    {
                        schemes[2]
                    }
                }
            };

            _mockRepository.Setup(x => x.QueryAsync<VillianObjectiveScheme>(VillianQueries.GetVillianObjectiveSchemes, CommandType.Text)).ReturnsAsync(villianObjectiveSchemes);

            var villianService = new VillianService(_configuration, _mockRepository.Object);
            var result = await villianService.GetVillianObjectiveSchemesAsync();

            result.Should().BeEquivalentTo(expected);
        }
    }
}

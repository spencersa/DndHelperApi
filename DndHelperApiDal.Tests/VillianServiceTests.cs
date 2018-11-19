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

        [Fact]
        public async Task Should_GetVillianMethodsAsync()
        {
            var expected = Fixture.CreateMany<VillianMethod>();
            _mockRepository.Setup(x => x.QueryAsync<VillianMethod>(VillianQueries.GetVillianMethods, CommandType.Text)).ReturnsAsync(expected);
            var villianService = new VillianService(_configuration, _mockRepository.Object);
            var result = await villianService.GetVillianMethodsAsync();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Should_GetVillianMethodsWithSubMethodsAsync()
        {
            var methods = Fixture.CreateMany<string>(2).ToList();
            var subMethods = Fixture.CreateMany<string>(3).ToList();

            var villianSubMethods = new List<VillianSubMethod>
            {
                new VillianSubMethod
                {
                    Id = 1,
                    Method = methods[0],
                    SubMethod = subMethods[0]
                },
                new VillianSubMethod
                {
                    Id = 1,
                    Method = methods[0],
                    SubMethod = subMethods[1]
                },
                new VillianSubMethod
                {
                    Id = 2,
                    Method = methods[1],
                    SubMethod = subMethods[2]
                }
            };

            var expected = new List<VillianMethodsWithSubMethods>
            {
                new VillianMethodsWithSubMethods
                {
                    Id = 1,
                    Method = methods[0],
                    SubMethods = new List<string>
                    {
                        subMethods[0],
                        subMethods[1]
                    }
                },
                new VillianMethodsWithSubMethods
                {
                    Id = 2,
                    Method = methods[1],
                    SubMethods = new List<string>
                    {
                        subMethods[2]
                    }
                }
            };

            _mockRepository.Setup(x => x.QueryAsync<VillianSubMethod>(VillianQueries.GetVillianSubMethods, CommandType.Text)).ReturnsAsync(villianSubMethods);

            var villianService = new VillianService(_configuration, _mockRepository.Object);
            var result = await villianService.GetVillianMethodsWithSubMethodsAsync();

            result.Should().BeEquivalentTo(expected);
        }
    }
}

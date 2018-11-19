using AutoFixture;
using DndHelperApi.Controllers;
using DndHelperApiCore.Tests;
using DndHelperApiDal.Models;
using DndHelperApiDal.Services;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DndHelperApi.Tests.ControllerTests
{
    public class VillianControllerTests : TestBase
    {
        private readonly Mock<IVillianService> _mockVillianService;

        public VillianControllerTests()
        {
            _mockVillianService = new Mock<IVillianService>();
        }

        [Fact]
        public async Task Should_GetVillianObjectives()
        {
            var expectedVillianObjectiveSchemes = Fixture.CreateMany<VillianObjective>();
            _mockVillianService.Setup(x => x.GetVillianObjectivesAsync()).ReturnsAsync(expectedVillianObjectiveSchemes);

            var villianController = new VillianController(_mockVillianService.Object);
            var result = await villianController.GetVillianObjectives();

            result.Should().BeEquivalentTo(expectedVillianObjectiveSchemes);
        }

        [Fact]
        public async Task Should_GetVillianObjectivesWithSchemes()
        {
            var expectedVillianObjectiveSchemes = Fixture.CreateMany<VillianObjectiveSchemes>();
            _mockVillianService.Setup(x => x.GetVillianObjectiveSchemesAsync()).ReturnsAsync(expectedVillianObjectiveSchemes);

            var villianController = new VillianController(_mockVillianService.Object);
            var result = await villianController.GetVillianObjectivesWithSchemes();

            result.Should().BeEquivalentTo(expectedVillianObjectiveSchemes);
        }

        [Fact]
        public async Task Should_GetVillianMethods()
        {
            var expectedVillianMethods = Fixture.CreateMany<VillianMethod>();
            _mockVillianService.Setup(x => x.GetVillianMethodsAsync()).ReturnsAsync(expectedVillianMethods);

            var villianController = new VillianController(_mockVillianService.Object);
            var result = await villianController.GetVillianMethods();

            result.Should().BeEquivalentTo(expectedVillianMethods);
        }

        [Fact]
        public async Task Should_GetVillianMethodsWithSubMethods()
        {
            var expectedVillianMethodsWithSubMethods = Fixture.CreateMany<VillianMethodsWithSubMethods>();
            _mockVillianService.Setup(x => x.GetVillianMethodsWithSubMethodsAsync()).ReturnsAsync(expectedVillianMethodsWithSubMethods);

            var villianController = new VillianController(_mockVillianService.Object);
            var result = await villianController.GetVillianMethodsWithSubMethods();

            result.Should().BeEquivalentTo(expectedVillianMethodsWithSubMethods);
        }

        [Fact]
        public async Task Should_GetVillianWeaknessesAsync()
        {
            var expectedVillianWeaknesss = Fixture.CreateMany<VillianWeakness>();
            _mockVillianService.Setup(x => x.GetVillianWeaknessesAsync()).ReturnsAsync(expectedVillianWeaknesss);

            var villianController = new VillianController(_mockVillianService.Object);
            var result = await villianController.GetVillianWeaknesses();

            result.Should().BeEquivalentTo(expectedVillianWeaknesss);
        }
    }
}

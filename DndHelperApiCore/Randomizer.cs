using DndHelperApiCore.Models;
using DndHelperApiDal.Models;
using DndHelperApiDal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DndHelperApiCore
{
    public interface IRandomizer
    {
        Task<Villian> GetRandomVillianAsync();
    }

    public class Randomizer : IRandomizer
    {
        private readonly IVillianService _villianService;

        public Randomizer(IVillianService villianService)
        {
            _villianService = villianService;
        }

        public async Task<Villian> GetRandomVillianAsync()
        {
            var tasks = new List<Task>();
            var objectivesTask = _villianService.GetVillianObjectiveSchemesAsync();
            var methodsTask = _villianService.GetVillianMethodsWithSubMethodsAsync();
            var weaknessTask = _villianService.GetVillianWeaknessesAsync();

            await Task.WhenAll(objectivesTask, methodsTask, weaknessTask);

            var objectives = objectivesTask.Result;
            var methods = methodsTask.Result;
            var weakness = weaknessTask.Result;

            return new Villian
            {
                Objective = DetermineObjective(objectives.ToList()),
                Method = DetermineMethod(methods.ToList()),
                Weakness = DetermineWeaknesses(weakness.ToList())
            };
        }

        private string DetermineObjective(List<VillianObjectiveSchemes> villianObjectiveSchemes)
        {
            var randomNumber = new Random();
            var objective = villianObjectiveSchemes[randomNumber.Next(0, villianObjectiveSchemes.Count)];

            var schemeIndex = randomNumber.Next(0, objective.Scheme.Count());
            return $"{objective.Objective}: {objective.Scheme.ToArray()[schemeIndex]}";
        }

        private string DetermineMethod(List<VillianMethodsWithSubMethods> villianMethodsWithSubMethods)
        {
            var randomNumber = new Random();
            var method = villianMethodsWithSubMethods[randomNumber.Next(0, villianMethodsWithSubMethods.Count)];

            return method.SubMethods == null ?
                method.Method :
                $"{method.Method}: {method.SubMethods.ToArray()[randomNumber.Next(0, method.SubMethods.Count())]}";
        }

        private string DetermineWeaknesses(List<VillianWeakness> villianWeakness)
        {
            return $"{villianWeakness[new Random().Next(0, villianWeakness.Count)].Weakness}";
        }
    }
}

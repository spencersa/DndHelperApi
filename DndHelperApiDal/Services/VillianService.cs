using DndHelperApiDal.Configurations;
using DndHelperApiDal.Models;
using DndHelperApiDal.Queries;
using DndHelperApiDal.Repositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DndHelperApiDal.Services
{
    public interface IVillianService
    {
        Task<IEnumerable<VillianObjective>> GetVillianObjectivesAsync();
        Task<IEnumerable<VillianObjectiveSchemes>> GetVillianObjectiveSchemesAsync();
    }

    public class VillianService : IVillianService
    {
        private readonly ConnectionConfiguration _connectionConfiguration;
        private readonly IRepository _repository;

        public VillianService(IOptions<ConnectionConfiguration> connectionConfiguration, IRepository repository)
        {
            _connectionConfiguration = connectionConfiguration.Value;
            _repository = repository;
        }

        public async Task<IEnumerable<VillianObjective>> GetVillianObjectivesAsync()
        {
            return await _repository.QueryAsync<VillianObjective>(VillianQueries.GetVillianObjectives, CommandType.Text);
        }

        public async Task<IEnumerable<VillianObjectiveSchemes>> GetVillianObjectiveSchemesAsync()
        {
            var queryResult = await _repository.QueryAsync<VillianObjectiveScheme>(VillianQueries.GetVillianObjectiveSchemes, CommandType.Text);

            var villianObjectiveWithSchemes = queryResult.GroupBy(x => x.Id).Select(x => new VillianObjectiveSchemes
            {
                Id = x.First().Id,
                Objective = x.First().Objective,
                Scheme = x.Select(scheme => scheme.Scheme)
            });

            return villianObjectiveWithSchemes;
        }
    }
}

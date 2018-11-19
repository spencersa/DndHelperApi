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
        Task<IEnumerable<VillianMethod>> GetVillianMethodsAsync();
        Task<IEnumerable<VillianMethodsWithSubMethods>> GetVillianMethodsWithSubMethodsAsync();
        Task<IEnumerable<VillianWeakness>> GetVillianWeaknessesAsync();
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

        public async Task<IEnumerable<VillianMethod>> GetVillianMethodsAsync()
        {
            return await _repository.QueryAsync<VillianMethod>(VillianQueries.GetVillianMethods, CommandType.Text);
        }

        public async Task<IEnumerable<VillianMethodsWithSubMethods>> GetVillianMethodsWithSubMethodsAsync()
        {
            var queryResult = await _repository.QueryAsync<VillianSubMethod>(VillianQueries.GetVillianSubMethods, CommandType.Text);

            var villianMethodsWithSubMethods = queryResult.GroupBy(x => x.Id).Select(x => new VillianMethodsWithSubMethods
            {
                Id = x.First().Id,
                Method = x.First().Method,
                SubMethods = x.Select(method => method.SubMethod)
            });

            return villianMethodsWithSubMethods;
        }

        public async Task<IEnumerable<VillianWeakness>> GetVillianWeaknessesAsync()
        {
            return await _repository.QueryAsync<VillianWeakness>(VillianQueries.GetVillianWeaknesses, CommandType.Text);
        }
    }
}

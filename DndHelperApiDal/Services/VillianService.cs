using Dapper;
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

        public async Task<IEnumerable<VillianObjectiveSchemes>> GetVillianObjectiveSchemesAsync()
        {
            var queryResult = await _repository.QueryAsync(async c =>
            {
                var parameters = new DynamicParameters();
                return (await c.QueryAsync<VillianObjective>(
                    VillianQueries.GetVillianObjectiveSchemes,
                    parameters,
                    commandType: CommandType.Text));
            });

            var villianObjectiveWithSchemes = queryResult.GroupBy(x => x.Objective).Select(x => new VillianObjectiveSchemes
            {
                Objective = x.First().Objective,
                Schemes = x.Select(scheme => scheme.Scheme)
            });

            return villianObjectiveWithSchemes;
        }
    }
}

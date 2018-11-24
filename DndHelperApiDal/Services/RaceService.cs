using DndHelperApiDal.Models;
using DndHelperApiDal.Queries;
using DndHelperApiDal.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DndHelperApiDal.Services
{
    public interface IRaceService
    {
        Task<IEnumerable<Race>> GetNpcRacesAsync();
    }

    public class RaceService : IRaceService
    {
        private readonly IRepository _repository;

        public RaceService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Race>> GetNpcRacesAsync()
        {
            return await _repository.QueryAsync<Race>(RaceQueries.SelectRace, CommandType.Text);
        }
    }
}

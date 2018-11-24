using DndHelperApiDal.Models;
using DndHelperApiDal.Queries;
using DndHelperApiDal.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DndHelperApiDal.Services
{
    public interface IAlignmentService
    {
        Task<IEnumerable<Alignment>> GetAlignments();
    }

    public class AlignmentService : IAlignmentService
    {
        private readonly IRepository _repository;

        public AlignmentService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Alignment>> GetAlignments()
        {
            return await _repository.QueryAsync<Alignment>(AlignmentQueries.SelectAlignment, CommandType.Text);
        }
    }
}

using BugTrackerCPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerCPAPI.Repositories
{
    public interface IBugRepository
    {
        Task<IEnumerable<Bug>> Get();
        Task Create(Bug bug);
        Task Delete(Guid id);
    }
    public class BugRepository : IBugRepository
    {
        private readonly IAbstractTableClient<BugEntity> _bugTable;
        private readonly IAssigneeRepository _assigneeRepository;

        public BugRepository(IAbstractTableClient<BugEntity> bugTable, IAssigneeRepository assigneeRepository)
        {
            _bugTable = bugTable;
            _assigneeRepository = assigneeRepository;
        }

        public async Task<IEnumerable<Bug>> Get()
        {
            var assignees = await _assigneeRepository.Get();
            var result = await _bugTable.Get<BugEntity>();
            return result.Select(result => result.ToModel(assignees));
        }

        public async Task Create(Bug bug) => await _bugTable.Create(bug.ToEntity());

        public async Task Delete(Guid id) => await _bugTable.Delete(id);
    }
}

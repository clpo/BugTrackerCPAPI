using BugTrackerCPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerCPAPI.Repositories
{
    public interface IAssigneeRepository
    {
        Task<IEnumerable<Assignee>> Get();
        Task Create(Assignee bug);
        Task Delete(Guid id);
    }

    public class AssigneeRepository : IAssigneeRepository
    {
        private readonly IAbstractTableClient<AssigneeEntity> _assigneeTable;

        public AssigneeRepository(IAbstractTableClient<AssigneeEntity> assigneeTable)
        {
            _assigneeTable = assigneeTable;
        }

        public async Task<IEnumerable<Assignee>> Get()
        {
            var results = await _assigneeTable.Get<AssigneeEntity>();
            return results.Select(result => result.ToModel());
        }

        public async Task Create(Assignee assignee) => await _assigneeTable.Create(assignee.ToEntity());

        public async Task Delete(Guid id) => await _assigneeTable.Delete(id);
    }
}

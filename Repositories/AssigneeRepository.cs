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
            await CreateStubData();
            var results = await _assigneeTable.Get<AssigneeEntity>();
            return results.Select(result => result.ToModel());
        }

        public async Task Create(Assignee assignee) => await _assigneeTable.Create(assignee.ToEntity());

        public async Task Delete(Guid id) => await _assigneeTable.Delete(id);

        private async Task CreateStubData()
        {
            //For the purpose of reviewing the tech task
            var isEmpty = await _assigneeTable.IsEmpty<AssigneeEntity>();
            if (isEmpty)
            {
                var assignee = new Assignee(name: "John Smith")
                {
                    Id = Guid.Parse("db8e1e46-8bc7-4d2d-99b7-1733898325f4")
                };
                await Create(assignee);
            }
        }
    }
}

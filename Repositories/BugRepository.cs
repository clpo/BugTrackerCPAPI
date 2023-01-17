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
            await CreateStubData();
            var assignees = await _assigneeRepository.Get();
            var result = await _bugTable.Get<BugEntity>();
            return result.Select(result => result.ToModel(assignees));
        }

        public async Task Create(Bug bug) => await _bugTable.Create(bug.ToEntity());

        public async Task Delete(Guid id) => await _bugTable.Delete(id);

        private async Task CreateStubData()
        {
            //For the purpose of reviewing the tech task
            var isEmpty = await _bugTable.IsEmpty<BugEntity>();
            if(isEmpty)
            {
                var bug = new Bug()
                {
                    Id = Guid.NewGuid(),
                    Name = "Fix issue with that thing that's broken",
                    Description = "The thing is broken and it needs fixing because...",
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Status = "Open",
                    Assignees = new[] { new Assignee(name: "John Smith") { Id = Guid.Parse("db8e1e46-8bc7-4d2d-99b7-1733898325f4") } }
                };
                await Create(bug);
            }
        }
    }
}

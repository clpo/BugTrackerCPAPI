using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using BugTrackerCPAPI.Repositories;

namespace BugTrackerCPAPI.Assignees.Get
{
    public class Function
    {
        private readonly IAssigneeRepository _assigneeRepository;

        public Function(IAssigneeRepository assigneeRepository)
        {
            _assigneeRepository = assigneeRepository;
        }

        [FunctionName("GetAssignees")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "assignee")] HttpRequest req)
        {
            var result = await _assigneeRepository.Get();
            return new OkObjectResult(result);
        }
    }
}

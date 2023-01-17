using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using BugTrackerCPAPI.Repositories;
using System;

namespace BugTrackerCPAPI.Assignees.Delete
{
    public class Function
    {
        private readonly IAssigneeRepository _assigneeRepository;

        public Function(IAssigneeRepository assigneeRepository)
        {
            _assigneeRepository = assigneeRepository;
        }

        [FunctionName("DeleteAssignee")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "assignee/{id}")] HttpRequest req, Guid id)
        {
            await _assigneeRepository.Delete(id);
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}

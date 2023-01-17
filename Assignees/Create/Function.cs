using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using BugTrackerCPAPI.Repositories;
using System.IO;
using BugTrackerCPAPI.Models;
using Newtonsoft.Json;

namespace BugTrackerCPAPI.Assignees.Create
{
    public class Function
    {
        private readonly IAssigneeRepository _assigneeRepository;

        public Function(IAssigneeRepository assigneeRepository)
        {
            _assigneeRepository = assigneeRepository;
        }

        [FunctionName("CreateAssignee")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "assignee")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Assignee data = JsonConvert.DeserializeObject<Assignee>(requestBody);

            await _assigneeRepository.Create(data);

            return new CreatedResult("", data);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using BugTrackerCPAPI.Repositories;

namespace BugTrackerCPAPI.Bugs.Get
{
    public class Function
    {
        private readonly IBugRepository _bugRepository;

        public Function(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        [FunctionName("GetBugs")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "bug")] HttpRequest req)
        {
            var result = await _bugRepository.Get();
            return new OkObjectResult(result);
        }
    }
}

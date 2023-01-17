using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using BugTrackerCPAPI.Repositories;
using System;

namespace BugTrackerCPAPI.Bugs.Delete
{
    public class Function
    {
        private readonly IBugRepository _bugRepository;

        public Function(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        [FunctionName("DeleteBug")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "bug/{id}")] HttpRequest req, Guid id)
        {
            await _bugRepository.Delete(id);
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}

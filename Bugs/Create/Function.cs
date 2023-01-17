using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using BugTrackerCPAPI.Repositories;
using BugTrackerCPAPI.Models;
using Newtonsoft.Json;
using System.IO;

namespace BugTrackerCPAPI.Bugs.Create
{
    public class Function
    {
        private readonly IBugRepository _bugRepository;

        public Function(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        [FunctionName("CreateBug")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "bug")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Bug data = JsonConvert.DeserializeObject<Bug>(requestBody);

            await _bugRepository.Create(data);

            return new CreatedResult("", data);
        }
    }
}

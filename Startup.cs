using Azure.Data.Tables;
using BugTrackerCPAPI.Models;
using BugTrackerCPAPI.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BugTrackerCPAPI.Startup))]

namespace BugTrackerCPAPI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var bugsClient = new AbstractTableClient<BugEntity>(new TableClient("UseDevelopmentStorage=true", "Bug"));
            var assigneeClient = new AbstractTableClient<AssigneeEntity>(new TableClient("UseDevelopmentStorage=true", "Assignee"));

            builder.Services.AddTransient<IAssigneeRepository, AssigneeRepository>(x => new AssigneeRepository(assigneeClient));
            builder.Services.AddTransient<IBugRepository, BugRepository>(x => new BugRepository(bugsClient, new AssigneeRepository(assigneeClient)));
        }
    }
}

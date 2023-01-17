using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackerCPAPI.Models
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public IEnumerable<Assignee> Assignees { get; set; }
        public string Status { get; set; }

        public Bug()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Created = DateTime.MinValue;
            LastUpdated = DateTime.MinValue;
            Assignees = new List<Assignee>();
            Status = string.Empty;
        }

        public static Bug Empty() { return new Bug(); }

        public BugEntity ToEntity()
        {
            return new BugEntity()
            {
                PartitionKey = Id.ToString(),
                RowKey = Id.ToString(),
                Name = Name,
                Description = Description,
                Created = Created.ToUniversalTime(),
                LastUpdated = LastUpdated.ToUniversalTime(),
                Status = Status,
                AssigneeIds = string.Join(",", Assignees.Select(x => x.Id.ToString()))
            };
        }

    }

    public class BugEntity : ITableEntity
    {
        public BugEntity(Guid id)
        {
            PartitionKey = id.ToString();
            RowKey = id.ToString();
        }

        public BugEntity() { }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string AssigneeIds { get; set; }
        public string Status { get; set; }

        public Bug ToModel(IEnumerable<Assignee> assignees)
        {
            var assigneesFromEntity = this.AssigneeIds.Split(',');
            return new Bug()
            {
                Id = Guid.Parse(this.PartitionKey),
                Name = this.Name,
                Description = this.Description,
                Created = this.Created,
                LastUpdated = this.LastUpdated,
                Status = this.Status,
                Assignees = assigneesFromEntity.Select(x => assignees.Single(assignee => assignee.Id == Guid.Parse(x)))
            };
        }
    }
}

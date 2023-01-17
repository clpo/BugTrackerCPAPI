using Azure;
using Azure.Data.Tables;
using System;

namespace BugTrackerCPAPI.Models
{
    public class Assignee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Assignee(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Assignee() { }

        public AssigneeEntity ToEntity()
        {
            return new AssigneeEntity() { PartitionKey = Id.ToString(), RowKey = Id.ToString(), Name = Name };
        }
    }

    public class AssigneeEntity : ITableEntity
    {
        public AssigneeEntity(Guid id)
        {
            PartitionKey = id.ToString();
            RowKey = id.ToString();
        }

        public AssigneeEntity() { }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Name { get; set; }

        public Assignee ToModel()
        {
            return new Assignee(name: this.Name) { Id = Guid.Parse(this.PartitionKey) };
        }
    }
}

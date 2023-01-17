using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerCPAPI.Repositories
{
    public interface IAbstractTableClient<T> where T : ITableEntity
    {
        Task<IEnumerable<T>> Get<T>() where T : class, ITableEntity, new();
        Task Create(T item);
        Task Delete(Guid id);
        Task<bool> IsEmpty<T>() where T : class, ITableEntity, new();
    }


    internal class AbstractTableClient<T> : IAbstractTableClient<T> where T: ITableEntity
    {
        private readonly TableClient _tableClient;

        public AbstractTableClient(TableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public async Task Create(T item)
        {
            await _tableClient.CreateIfNotExistsAsync();
            await _tableClient.AddEntityAsync(item);
        }

        public async Task Delete(Guid id)
        {
            await _tableClient.CreateIfNotExistsAsync();
            await _tableClient.DeleteEntityAsync(id.ToString(), id.ToString());
        }

        public async Task<IEnumerable<T>> Get<T>() where T : class, ITableEntity, new()
        {
            await _tableClient.CreateIfNotExistsAsync();
            IAsyncEnumerable<Page<T>> result;
            IList<T> items = new List<T>();

            result = _tableClient.QueryAsync<T>().AsPages();
            await foreach (var page in result)
            {
                foreach (var pageValue in page.Values)
                {
                    items.Add(pageValue);
                }
            }
            return items;
        }

        public async Task<bool> IsEmpty<T>() where T : class, ITableEntity, new()
        {
            await _tableClient.CreateIfNotExistsAsync();
            var allItems = await Get<T>();
            return allItems.Count() == 0;
        }
    }
}

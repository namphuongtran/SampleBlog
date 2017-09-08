using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleBlog.Repositories.Interface;

namespace SampleBlog.Repositories
{
    public class InMemoryRepository<T> : ICommonRepository<T> where T : IEntity
    {
        public static List<T> _allItems = new List<T>();
        public List<T> AllItems
        {
            get
            {
                return _allItems;
            }
        }

        public IQueryable<T> AsQuery()
        {
            return AllItems.AsQueryable();
        }

        public void Add(T item)
        {
            AllItems.Add(item);
            item.Id = AllItems.Max(pp => pp.Id) + 1;
        }

        public void Remove(T item)
        {
            AllItems.RemoveAll(i => i.Id == item.Id);
        }
    }
}

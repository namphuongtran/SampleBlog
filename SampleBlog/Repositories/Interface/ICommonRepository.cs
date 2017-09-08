using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBlog.Repositories.Interface
{
    public interface ICommonRepository<T> where T: IEntity
    {
        IQueryable<T> AsQuery();
        void Add(T item);
        void Remove(T item);
    }
}

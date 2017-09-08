using SampleBlog.Repositories.Entities;
using System.Linq;

namespace SampleBlog.Repositories.Interface
{
    /// <summary>
    /// Interface for storage abstraction (repository) of tags
    /// </summary>
    public interface ITagRepository : ICommonRepository<Tag>
    {
        IQueryable<Tag> AsQuery();
        void Add(Tag item);
        void Remove(Tag item);
    }
}

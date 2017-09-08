using System.Linq;

using SampleBlog.Repositories.Entities;

namespace SampleBlog.Repositories.Interface
{
    /// <summary>
    /// Interface for storage abstraction (repository) of posts
    /// </summary>
    public interface IPostRepository : ICommonRepository<Post>
    {
        IQueryable<Post> AsQuery();
        void Add(Post item);
        void Remove(Post item);
    }
}
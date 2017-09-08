using SampleBlog.Repositories.Entities;
using SampleBlog.Repositories.Interface;
using System.Linq;

namespace SampleBlog.Repositories
{
    /// <summary>
    /// Implementation for storage abstraction (repository) of posts bound to an in-memory repository
    /// </summary>
    public class PostRepository : InMemoryRepository<Post>, IPostRepository
    {
    }
}
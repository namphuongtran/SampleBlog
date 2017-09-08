using SampleBlog.Repositories.Entities;
using SampleBlog.Repositories.Interface;
using System.Linq;

namespace SampleBlog.Repositories
{
    /// <summary>
    /// Implementation for storage abstraction (repository) of tags bound to in-memory repository
    /// </summary>
    public class TagRepository : InMemoryRepository<Tag>, ITagRepository
    {
    }
}
using SampleBlog.Repositories.Interface;
using System.Collections.Generic;

namespace SampleBlog.Repositories.Entities
{
    /// <summary>
    /// Database entity for post tag
    /// </summary>
    public class Tag: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
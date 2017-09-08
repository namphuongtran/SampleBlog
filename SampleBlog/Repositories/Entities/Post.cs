using SampleBlog.Models;
using SampleBlog.Repositories.Interface;
using System;
using System.Collections.Generic;

namespace SampleBlog.Repositories.Entities
{
    /// <summary>
    /// Database entity for post
    /// </summary>
    public class Post: IEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ? DateModified { get; set; }
        public string CreatedById { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
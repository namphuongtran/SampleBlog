using System;
using System.Collections.Generic;

namespace SampleBlog.Models
{
    /// <summary>
    /// Detailed post data representing post in external web communications
    /// </summary>
    public class PostDetailsModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ? DateModified { get; set; }
        public string CreatedById { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<string> Tags { get; set; }
    }
}
using System;

namespace SampleBlog.Models
{
    /// <summary>
    /// Short post description representing post list item in external web communications
    /// </summary>
    public class PostListItemModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedById { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
    }
}
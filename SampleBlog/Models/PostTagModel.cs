using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBlog.Models
{
    /// <summary>
    /// Post tag reference which is used in external web operations
    /// </summary>
    public class PostTagModel
    {
        public int PostId { get; set; }
        public string Tag { get; set; }
    }
}
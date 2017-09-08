using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBlog.Managers.Interface
{
    /// <summary>
    /// Interface for tags-related business logic methods
    /// </summary>
    public interface ITagManager
    {
        void TagPost(int postId, string tag);
        void UntagPost(int postId, string tag);
    }
}
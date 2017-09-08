using SampleBlog.Managers;
using SampleBlog.Managers.Interface;
using SampleBlog.Models;
using SampleBlog.Repositories.Entities;
using System.Web.Http;

namespace SampleBlog.Controllers
{
    /// <summary>
    /// API controller to manage blog post tags
    /// </summary>
    public class TagController : ApiController
    {
        private ITagManager _tagManager;

        public TagController(ITagManager tagManager)
        {
            _tagManager = tagManager;
        }

        // POST: api/Tag
        // PostId and Tag name must be specified in request body
        [HttpPost]
        public void Post([FromBody]PostTagModel pt)
        {
            _tagManager.TagPost(pt.PostId, pt.Tag);
        }

        // DELETE: api/Tag/5/some%30post
        [HttpDelete]
        public void Delete(int postId, string tag)
        {
            _tagManager.UntagPost(postId, tag);
        }
    }
}

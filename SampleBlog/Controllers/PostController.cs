using AutoMapper;
using SampleBlog.Managers;
using SampleBlog.Managers.Interface;
using SampleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SampleBlog.Controllers
{
    /// <summary>
    /// API controller to manage blog posts
    /// </summary>
    public class PostController : ApiController
    {
        private IPostManager _postManager;

        public PostController(IPostManager postManager)
        {
            _postManager = postManager;
        }

        // GET: api/Post
        public IEnumerable<PostListItemModel> Get()
        {
            return _postManager.List();
        }

        // GET: api/Post/5
        public PostDetailsModel Get(int id)
        {
            return _postManager.Get(id);
        }

        // POST: api/Post
        // A blog post must be speicfied in request body
        public int Post([FromBody]PostDetailsModel post)
        {
            post.DateCreated = DateTime.UtcNow;
            if (User != null)
                post.CreatedById = User.Identity.Name;
            _postManager.Save(post);
            return post.Id;
        }

        // PUT: api/Post
        // A blog post must be speicfied in request body
        [HttpPut]
        public void Put([FromBody]PostDetailsModel post)
        {
            _postManager.Save(post);
        }

        // DELETE: api/Post/5
        public void Delete(int id)
        {
            _postManager.Remove(id);
        }
    }
}

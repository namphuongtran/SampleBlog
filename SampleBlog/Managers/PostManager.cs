using AutoMapper;
using SampleBlog.Managers.Interface;
using SampleBlog.Models;
using SampleBlog.Repositories.Interface;
using SampleBlog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBlog.Managers
{
    /// <summary>
    /// Implementation for post-related business logic methods
    /// </summary>
    public class PostManager : IPostManager
    {
        private IPostRepository _postRepository;

        public PostManager(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void Save(PostDetailsModel postDetails)
        {
            if (string.IsNullOrEmpty(postDetails.Title))
            {
                throw new ArgumentException("Post title is empty");
            }

            if (string.IsNullOrEmpty(postDetails.Content))
            {
                throw new ArgumentException("Post content is empty");
            }

            Repositories.Entities.Post savedEntity;

            if (postDetails.Id > 0)
            {
                var existingPost = _postRepository.AsQuery().FirstOrDefault(p => p.Id == postDetails.Id);
                if (existingPost == null)
                {
                    throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", postDetails.Id));
                }

                savedEntity = Mapper.Map<SampleBlog.Models.PostDetailsModel, Repositories.Entities.Post>(postDetails, existingPost);
                savedEntity.DateModified = DateTime.UtcNow;
            }
            else
            {
                savedEntity = Mapper.Map<Repositories.Entities.Post>(postDetails);
                _postRepository.Add(savedEntity);
            }

            postDetails.Id = savedEntity.Id;
        }

        public PostDetailsModel Get(int id)
        {
            PostDetailsModel postDetails = new PostDetailsModel();
            if (id <= 0)
                throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", id));
            else
            {
                var post = _postRepository.AsQuery().FirstOrDefault(p => p.Id == id);
                if (post != null)
                {
                    postDetails = Mapper.Map<Repositories.Entities.Post, SampleBlog.Models.PostDetailsModel>(post, postDetails);
                    if (postDetails != null)
                    {
                        foreach (var item in post.Tags)
                        {
                            if (item.Posts.Where(k => k.Id == post.Id).FirstOrDefault() != null)
                                postDetails.Tags.Add(item.Name);
                        }
                    }
                }
            }
            return postDetails;
            // please add implementation here

            // method should call repository, get what's required, 
            // map to return object via AutoMapper (take a look at its usage in other methods in this class)
            // be careful with Tags property - please map it manually
            // and throw ArgumentException when postId is not valid
        }

        public ICollection<PostListItemModel> List()
        {
            return Mapper.Map<List<PostListItemModel>>(_postRepository.AsQuery());
        }

        public void Remove(int id)
        {
            var post = _postRepository.AsQuery().FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", id));
            }
            _postRepository.Remove(post);
        }
    }
}
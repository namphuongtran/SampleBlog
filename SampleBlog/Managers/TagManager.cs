using SampleBlog.Managers.Interface;
using SampleBlog.Repositories;
using SampleBlog.Repositories.Interface;
using System;
using System.Linq;

namespace SampleBlog.Managers
{
    /// <summary>
    /// Implementation for tags-related business logic methods
    /// </summary>
    public class TagManager : ITagManager
    {
        private ITagRepository _tagRepository;
        private IPostRepository _postRepository;

        public TagManager(ITagRepository tagRepository, IPostRepository postRepository)
        {
            _tagRepository = tagRepository;
            _postRepository = postRepository;
        }

        public void TagPost(int postId, string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("Tag name is invalid");
            }

            var post = _postRepository.AsQuery().FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", postId));
            }

            var tagEntity = _tagRepository.AsQuery().FirstOrDefault(t => t.Name == tag);

            // check if a given post is already tagged with tag specified
            if (tagEntity == null || !post.Tags.Any(pt => pt.Id == tagEntity.Id))
            {
                if (tagEntity == null)
                {
                    tagEntity = new Repositories.Entities.Tag
                    {
                        Name = tag
                    };
                    _tagRepository.Add(tagEntity);
                }
                post.Tags.Add(tagEntity);
            }
        }

        public void UntagPost(int postId, string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("Tag name is invalid");
            }
            var post = _postRepository.AsQuery().FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                throw new ArgumentException(string.Format("Specified post (id={0}) does not exist", postId));
            }
            var tagEntity = _tagRepository.AsQuery().FirstOrDefault(t => t.Name == tag);
            if (tagEntity == null)
            {
                throw new ArgumentException(string.Format("Specified tag (tagname={0}) does not exist", tag));
            }
            else
            {

                foreach (var item in tagEntity.Posts)
                {
                    if (item.Id == post.Id)
                    {
                        _tagRepository.Remove(tagEntity);
                    }
                }
            }
            // please add implementation here

            // method should delete proper tag entity object from post entity, use repository and LINQ queries to get those
            // handle unexpected scenarios: tag argument not valid, post not found, tag is not belonging to post
            // with ArgumentException being thrown            
        }
    }
}
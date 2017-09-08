using System;
using System.Collections.Generic;
using System.Linq;

using SampleBlog.Controllers;
using SampleBlog.Managers.Interface;
using SampleBlog.Models;

using FluentAssertions;
using Moq;
using Ninject;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SampleBlog.Tests.Controllers
{
    /// <summary>
    /// Unit tests of Post API controllers
    /// 
    /// Since API controllers do not contain any logic just checks that underlying implementation methods are being called
    /// </summary>
    [TestFixture]
    public class PostControllerTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void GetList()
        {
            var postManagerMock = new Mock<IPostManager>();
            postManagerMock.Setup(pm => pm.List()).Returns(
                new List<PostListItemModel>
                { 
                    new PostListItemModel() 
                });

            var controller = new PostController(postManagerMock.Object);
            var result = controller.Get();

            postManagerMock.Verify(p => p.List(), Times.Once);
            result.Count().Should().Be(1);
        }

        [Test]
        public void GetDetails()
        {
            var postId = 1;
            var postManagerMock = new Mock<IPostManager>();
            postManagerMock.Setup(pm => pm.Get(It.IsAny<int>())).Returns(new PostDetailsModel());

            var controller = new PostController(postManagerMock.Object);
            var result = controller.Get(postId);

            postManagerMock.Verify(p => p.Get(postId), Times.Once);
            result.Should().NotBeNull();
        }

        [Test]
        public void NewPost()
        {
            var postManagerMock = new Mock<IPostManager>();
            postManagerMock.Setup(pm => pm.Save(It.IsAny<PostDetailsModel>()));

            var controller = new PostController(postManagerMock.Object);
            controller.Post(new PostDetailsModel());

            postManagerMock.Verify(p => p.Save(It.IsAny<PostDetailsModel>()), Times.Once);
        }

        [Test]
        public void SavePost()
        {
            var postId = 1;
            var postManagerMock = new Mock<IPostManager>();
            postManagerMock.Setup(pm => pm.Save(It.IsAny<PostDetailsModel>()));

            var controller = new PostController(postManagerMock.Object);
            controller.Put(new PostDetailsModel { Id = postId });

            postManagerMock.Verify(p => p.Save(It.IsAny<PostDetailsModel>()), Times.Once);
        }

        [Test]
        public void RemovePost()
        {
            var postId = 1;
            var postManagerMock = new Mock<IPostManager>();
            postManagerMock.Setup(pm => pm.Remove(It.IsAny<int>()));

            var controller = new PostController(postManagerMock.Object);
            controller.Delete(postId);

            postManagerMock.Verify(p => p.Remove(postId), Times.Once);
        }    
    }
}

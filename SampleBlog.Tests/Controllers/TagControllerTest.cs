using SampleBlog.Controllers;
using SampleBlog.Managers.Interface;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SampleBlog.Tests.Controllers
{
    /// <summary>
    /// Unit tests of Tag API controller
    /// 
    /// Since API controllers do not contain any logic just checks that underlying implementation methods are being called
    /// </summary>
    [TestFixture]
    public class TagControllerTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void Post()
        {
            var postId = 1;
            var tag = "1";
            var tagManagerMock = new Mock<ITagManager>();
            tagManagerMock.Setup(pm => pm.TagPost(It.IsAny<int>(), It.IsAny<string>()));

            var controller = new TagController(tagManagerMock.Object);
            controller.Post(new Models.PostTagModel { PostId = postId, Tag = tag });

            tagManagerMock.Verify(p => p.TagPost(postId, tag), Times.Once);
        }

        [Test]
        public void Delete()
        {
            var postId = 1;
            var tag = "1";
            var tagManagerMock = new Mock<ITagManager>();
            tagManagerMock.Setup(pm => pm.UntagPost(It.IsAny<int>(), It.IsAny<string>()));

            var controller = new TagController(tagManagerMock.Object);
            controller.Delete(postId, tag);

            tagManagerMock.Verify(p => p.UntagPost(postId, tag), Times.Once);
        }
    }
}

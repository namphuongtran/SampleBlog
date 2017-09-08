using NUnit.Framework;
using SampleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SampleBlog.Tests.Integration
{
    /// <summary>
    /// Test fixture for integration testing of tag API
    /// </summary>
    [TestFixture]
    public class TagApiTest : BaseIntegrationTest
    {
        /// <summary>
        /// Tests both tag API methods and checks for sanity in between
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task IntegrationAllTagMethods()
        {
            using (var httpClient = new HttpClient())
            {
                var newPost = new
                {
                    Title = "SampleTitleForTags",
                    Content = "SampleContentForTags"
                };

                // Create new post to test tagging on it
                var createResult = await httpClient.PostAsJsonAsync(ApiAction("Post"), newPost);
                Assert.AreEqual(HttpStatusCode.OK, createResult.StatusCode, "POST API method failed (for post)");
                var newPostId = int.Parse(createResult.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(newPostId > 0);

                var newTag = new
                {
                    PostId = newPostId,
                    Tag = "Sample Tag"
                };

                var tagResult = await httpClient.PostAsJsonAsync(ApiAction("Tag"), newTag);
                Assert.AreEqual(HttpStatusCode.NoContent, tagResult.StatusCode, "POST API method failed (for tag)");

                // Load post details to check if it contains tag
                var getResult = await httpClient.GetAsync(ApiAction(string.Format("Post/{0}", newPostId)));
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode, "GET API method failed (for post)");
                var postLoaded = new JavaScriptSerializer().Deserialize<PostDetailsModel>(getResult.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(postLoaded.Tags.Contains(newTag.Tag));

                var untagResult = await httpClient.DeleteAsync(ApiAction(string.Format("Tag/{0}/{1}", newPostId, Uri.EscapeUriString(newTag.Tag))));
                Assert.AreEqual(HttpStatusCode.NoContent, untagResult.StatusCode, "DELETE API method failed (for tag)");

                // Load post details to check that it does not contain tag anymore
                var getResult2 = await httpClient.GetAsync(ApiAction(string.Format("Post/{0}", newPostId)));
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode, "GET API method failed (for post)");
                var postLoaded2 = new JavaScriptSerializer().Deserialize<PostDetailsModel>(getResult2.Content.ReadAsStringAsync().Result);
                Assert.IsFalse(postLoaded2.Tags.Contains(newTag.Tag));

            }
        }
    }
}

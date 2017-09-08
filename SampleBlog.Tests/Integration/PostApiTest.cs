using Microsoft.Owin.Hosting;
using NUnit.Framework;
using SampleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SampleBlog.Tests.Integration
{
    /// <summary>
    /// Test fixture for integration testing post API
    /// </summary>
    [TestFixture]
    public class PostApiTest : BaseIntegrationTest
    {
        /// <summary>
        /// Performs checks of all post-related API methods one by one with checks of sanity in between
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task IntegrationAllPostMethods()
        {
            using (var httpClient = new HttpClient())
            {
                var newPost = new 
                {
                    Title = "SampleTitle",
                    Content = "SampleContent"
                };

                // New post creation test
                var postResult = await httpClient.PostAsJsonAsync(ApiAction("Post"), newPost);
                Assert.AreEqual(HttpStatusCode.OK, postResult.StatusCode, "POST API method failed");
                var newPostId = int.Parse(postResult.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(newPostId > 0);

                // Load newly created post test
                var getResult = await httpClient.GetAsync(ApiAction(string.Format("Post/{0}", newPostId)));
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode, "GET/id API method failed");
                var postLoaded = new JavaScriptSerializer().Deserialize<PostDetailsModel>(getResult.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(newPostId, postLoaded.Id);
                Assert.AreEqual(newPost.Title, postLoaded.Title);
                Assert.AreEqual(newPost.Content, postLoaded.Content);
                Assert.IsNull(postLoaded.DateModified);
                Assert.IsTrue(postLoaded.DateCreated > DateTime.MinValue);

                // List posts and check that newly created post is there
                var listResult = await httpClient.GetAsync(ApiAction("Post"));
                Assert.AreEqual(HttpStatusCode.OK, listResult.StatusCode, "GET API method failed");
                var listItems = new JavaScriptSerializer().Deserialize<List<PostListItemModel>>(listResult.Content.ReadAsStringAsync().Result);
                var postListItemLoaded = listItems.FirstOrDefault(p => p.Id == newPostId);
                Assert.IsNotNull(postListItemLoaded);
                Assert.AreEqual(newPostId, postListItemLoaded.Id);
                Assert.AreEqual(newPost.Title, postListItemLoaded.Title);
                Assert.IsTrue(postListItemLoaded.DateCreated > DateTime.MinValue);

                // Modifying post and saving it
                postLoaded.Title += "Modified";
                postLoaded.Content += "Modified";
                var putResult = await httpClient.PutAsJsonAsync(ApiAction("Post"), postLoaded);
                Assert.AreEqual(HttpStatusCode.NoContent, putResult.StatusCode, "PUT API method failed");

                // Loading the post again to ensure modifications saved
                var getResult2 = await httpClient.GetAsync(ApiAction(string.Format("Post/{0}", newPostId)));
                Assert.AreEqual(HttpStatusCode.OK, getResult2.StatusCode, "GET API method failed");
                var postLoaded2 = new JavaScriptSerializer().Deserialize<PostDetailsModel>(getResult2.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(postLoaded.Title, postLoaded2.Title);
                Assert.AreEqual(postLoaded.Content, postLoaded2.Content);
                Assert.IsNotNull(postLoaded2.DateModified);
                Assert.IsTrue(postLoaded2.DateCreated > DateTime.MinValue);

                // Deleting the post we just created
                var deleteResult = await httpClient.DeleteAsync(ApiAction(string.Format("Post/{0}", newPostId)));
                Assert.AreEqual(HttpStatusCode.NoContent, deleteResult.StatusCode, "DELETE API method failed");

                // List all posts again to ensure that post is deleted
                var listResult2 = await httpClient.GetAsync(ApiAction("Post"));
                Assert.AreEqual(HttpStatusCode.OK, listResult2.StatusCode, "GET API method failed");
                var listItems2 = new JavaScriptSerializer().Deserialize<List<PostListItemModel>>(listResult2.Content.ReadAsStringAsync().Result);
                Assert.IsFalse(listItems2.Any(p => p.Id == newPostId));

            }
        }
    }
}

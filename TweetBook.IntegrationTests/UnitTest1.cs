using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Tweetbook;
using Tweetbook.Contracts.V1;
using Xunit;

namespace TweetBook.IntegrationTests
{
    public class UnitTest1
    {
        private readonly HttpClient _client;

        public UnitTest1()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient(); //verdiðimiz Startup'a ait projeyi in  memory olarak tutar


        }

        //[Fact]
        public async void Test1()
        {
            var response = await _client.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", "1"));
        }
    }
}

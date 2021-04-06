using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;

namespace FunctionalTests.Controllers
{
    public class ControllersTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _htpClient;
        public ControllersTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _htpClient = webApplicationFactory.CreateClient();
        }
        public class Get : ControllersTests
        {
            public Get(WebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory) { }
            [Fact]
            public async Task Should_respond_a_status_200_OK()
            {
                var result = await _htpClient.GetAsync("/api/values");
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
            [Fact]
            public async Task Should_respond_the_expected_string()
            {
                var result = await _htpClient.GetAsync("/api/values");
                var contentText = await result.Content.ReadAsStringAsync();
                var content = JsonSerializer.Deserialize<string[]>(contentText);

                Assert.Collection(content, x => Assert.Equal("value1", x), x => Assert.Equal("valus2", x));
            }
        }
    }
}

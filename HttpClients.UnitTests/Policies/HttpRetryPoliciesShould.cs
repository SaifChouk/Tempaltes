//using HttpClients.Config;
//using HttpClients.Policies;
//using HttpClients.TokenCLients;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Moq.Protected;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace HttpClients.UnitTests.Policies
//{
//    public class HttpRetryPoliciesShould
//    {
//        const string TestClient = "TestClient";
//        private readonly PolicyConfig _policyConfig;
//        private readonly ILogger _logger;
//        public HttpRetryPoliciesShould()
//        {
//            _logger = Mock.Of<ILogger>();
//            _policyConfig = new PolicyConfig { BreakDuration = 10, RetryCount = 3 };
//        }
//        [Fact]
//        public async Task RetryWhenReceivingTransientError()
//        {
//            // ARRANGE
//            IServiceCollection sc = new ServiceCollection();
//            var inMemorySettings = new Dictionary<string, string> {
//    {"PolicyConfig:BreakDuration", "10"},
//    {"PolicyConfig:RetryCount", "3"},
//};

//            IConfiguration configuration = new ConfigurationBuilder()
//                .AddInMemoryCollection(inMemorySettings)
//                .Build();
//            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict).Protected()
//               // Setup the PROTECTED method to mock
//               .SetupSequence<Task<HttpResponseMessage>>(
//                  "SendAsync",
//                  ItExpr.IsAny<HttpRequestMessage>(),
//                  ItExpr.IsAny<CancellationToken>()
//               );
//            handlerMock.Throws(TestHelper.CreateRefitException(HttpMethod.Get, HttpStatusCode.InternalServerError))
//                .Throws(TestHelper.CreateRefitException(HttpMethod.Get, HttpStatusCode.ServiceUnavailable))
//                .Throws(TestHelper.CreateRefitException(HttpMethod.Get, HttpStatusCode.RequestTimeout))
//                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

//            var httpClient = new HttpClient(handlerMock.ob);
//            httpClient.BaseAddress = new Uri(@"http://some.address.com/v1/");
//            httpClient.DefaultRequestHeaders.Accept.Clear();
//            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));


//            var retryPolicy = HttpRetryPolicies.GetHttpRetryPolicy(_logger, _policyConfig);

//            // ACT
//            var result = await retryPolicy.ExecuteAsync(async () => await httpClient.GetAsync("/").ConfigureAwait(false)).ConfigureAwait(false);

//            // ASSERT
//            handlerMock.Verify(a=>a., Times.Exactly(1));
//            httpClient.GetAsync.shoul
//            authResult.Should().NotBeNull();
//            authResult.access_token.Should().NotBeNullOrEmpty().And.Be(TEST_ACCESS_TOKEN);

//            // The API Should have called GetAccessToken exactly 3 times: 2 times it received an exception,
//            // the third time a correct result
//            mockApi.Verify(x => x.GetAccessToken(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
//        }
//    }
//}

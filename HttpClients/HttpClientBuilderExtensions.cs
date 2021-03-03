using HttpClients.Config;
using HttpClients.Handlers;
using HttpClients.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HttpClients
{
    public static class HttpClientBuilderExtensions
    {
        #region Add default cooptatio polcies
        public static IHttpClientBuilder AddDefaultPolicyHandlers(this IHttpClientBuilder httpClientBuilder, IOptions<PolicyConfig> policyConfigOptions, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            var retryLogger = loggerFactory.CreateLogger("PollyHttpRetryPoliciesLogger");
            var circuitBreakerLogger = loggerFactory.CreateLogger("PollyHttpCircuitBreakerPoliciesLogger");

            var policyConfig = policyConfigOptions.Value;

            var circuitBreakerPolicyConfig = (ICircuitBreakerPolicyConfig)policyConfig;
            var retryPolicyConfig = (IRetryPolicyConfig)policyConfig;

            return httpClientBuilder.AddRetryPolicyHandler(retryPolicyConfig)
                                    .AddCircuitBreakerHandler(circuitBreakerPolicyConfig);
        }
        public static IHttpClientBuilder AddDefaultPolicyHandlers(this IHttpClientBuilder httpClientBuilder, string policySectionName, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            var retryLogger = loggerFactory.CreateLogger("PollyHttpRetryPoliciesLogger");
            var circuitBreakerLogger = loggerFactory.CreateLogger("PollyHttpCircuitBreakerPoliciesLogger");

            var policyConfig = new PolicyConfig();
            configuration.Bind(policySectionName, policyConfig);

            var circuitBreakerPolicyConfig = (ICircuitBreakerPolicyConfig)policyConfig;
            var retryPolicyConfig = (IRetryPolicyConfig)policyConfig;

            return httpClientBuilder.AddRetryPolicyHandler(retryPolicyConfig)
                                    .AddCircuitBreakerHandler(circuitBreakerPolicyConfig);
        }

        public static IHttpClientBuilder AddDefaultPolicyHandlers(this IHttpClientBuilder httpClientBuilder, ILogger _logger, IConfiguration configuration)
        {


            var policyConfig = new PolicyConfig();
            configuration.Bind("PolicyConfig", policyConfig);

            var circuitBreakerPolicyConfig = (ICircuitBreakerPolicyConfig)policyConfig;
            var retryPolicyConfig = (IRetryPolicyConfig)policyConfig;

            return httpClientBuilder.AddRetryPolicyHandler(retryPolicyConfig)
                                    .AddCircuitBreakerHandler(circuitBreakerPolicyConfig);
        }
        #endregion

        #region add single policy
        public static IHttpClientBuilder AddRetryPolicyHandler(this IHttpClientBuilder httpClientBuilder, IRetryPolicyConfig retryPolicyConfig)
        {
            return httpClientBuilder.AddPolicyHandler((services, request) =>
            {
                var loggerFactory = services.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("PollyHttpRetryPoliciesLogger");
                if (request.Method == HttpMethod.Get)
                {
                    return HttpRetryPolicies.GetHttpRetryPolicy(logger, retryPolicyConfig);
                }

                if (request.Method == HttpMethod.Post)
                {
                    return HttpNoOpPolicies.GetHttpNoOpPolicy();
                }

                return HttpRetryPolicies.GetHttpRetryPolicy(logger, retryPolicyConfig);

            });
        }

        public static IHttpClientBuilder AddCircuitBreakerHandler(this IHttpClientBuilder httpClientBuilder, ICircuitBreakerPolicyConfig circuitBreakerPolicyConfig)
        {
            return httpClientBuilder.AddPolicyHandler((services, request) =>
            {
                var loggerFactory = services.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("PollyHttpCircuitBreakerPoliciesLogger");
                return HttpCircuitBreakerPolicies.GetHttpCircuitBreakerPolicy(logger, circuitBreakerPolicyConfig);
            });

        }
        #endregion

        #region add default cooptatio message handlers
        public static IHttpClientBuilder AddDefaultMessageHandlers(this IHttpClientBuilder httpClientBuilder)
        {
            return httpClientBuilder
                .AddHttpMessageHandler<DefaultLoggingHttpMessageHandler>()
                .AddHttpMessageHandler<CorrelationIdHttpMessageHandler>();
        }
        #endregion
    }
}

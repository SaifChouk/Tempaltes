using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Correlation
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICorrelationAccessor _correlationAccessor;
        private readonly CorrelationConfiguration _configuration;

        public CorrelationIdMiddleware(RequestDelegate next,
            ICorrelationAccessor correlationAccessor,
            IOptions<CorrelationConfiguration> configuration)
        {
            _next = next;
            _correlationAccessor = correlationAccessor;
            _configuration = configuration.Value;
        }

        public Task Invoke(HttpContext context)
        {
            ExtractCorrelationId(context);

            return _next(context);
        }

        private void ExtractCorrelationId(HttpContext context)
        {
            context.Request.Headers
                .TryGetValue(_configuration.HeaderName, out var values);

            var value = values.FirstOrDefault();

            if (value != null)
            {
                _correlationAccessor.SetId(value);
            }
        }

    }
}

using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.UnitTests
{
    public static class TestHelper
    {
        public static ApiException CreateRefitException(HttpMethod method, HttpStatusCode httpStatusCode) =>
             ApiException.Create(
                new HttpRequestMessage(),
                method,
                new HttpResponseMessage(httpStatusCode), new RefitSettings()).Result;


    }
}

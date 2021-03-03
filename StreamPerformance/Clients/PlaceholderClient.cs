using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StreamPerformance
{
    [MemoryDiagnoser]
    public class PlaceholderClient : IPlaceholderClient
    {
        //private readonly HttpClient _httpClient;
        //private readonly ILogger<PlaceholderClient> _logger;

        //public PlaceholderClient(HttpClient httpClient, ILogger<PlaceholderClient> logger)
        //{
        //    _httpClient = httpClient;
        //    _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        //    _logger = logger;
        //}
        private HttpClient _httpClient;
        [GlobalSetup]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
        public async Task<List<Album>> GetAlbums()
        {
            var response = await _httpClient.GetAsync("/albums");
            var result = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<List<Album>>(result);
        }

        public async Task<List<Album>> GetAlbumsAsStream()
        {
            using (var response = await _httpClient.GetAsync("/albums", HttpCompletionOption.ResponseHeadersRead))
            {

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<Album>>(stream);
                }
            }

        }
        public async Task<List<Album>> GetAlbumsAsStreamSimplified()
        {
            using var stream = await _httpClient.GetStreamAsync("/albums");

            return await System.Text.Json.JsonSerializer.DeserializeAsync<List<Album>>(stream);
        }

        public async Task<List<Comment>> GetComments()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetCommentsAsStream()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetCommentsAsStreamSimplified()
        {
            throw new NotImplementedException();
        }

        [Benchmark(Baseline = true)]
        public async Task<List<Photo>> GetPhotos()
        {
            var response = await _httpClient.GetAsync("/photos");
            var result = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<List<Photo>>(result);
        }
        [Benchmark]
        public async Task<List<Photo>> GetPhotosAsStream()
        {
            using (var response = await _httpClient.GetAsync("/albums", HttpCompletionOption.ResponseHeadersRead))
            {

                using (var stream = await response.Content.ReadAsStreamAsync())
                {

                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<Photo>>(stream);
                }
            }
        }
        [Benchmark]
        public async Task<List<Photo>> GetPhotosAsStreamSimplified()
        {
            using var stream = await _httpClient.GetStreamAsync("/albums");

            return await System.Text.Json.JsonSerializer.DeserializeAsync<List<Photo>>(stream);
        }

        public async Task<List<Post>> GetPost()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetPostAsStream()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetPostAsStreamSimplified()
        {
            throw new NotImplementedException();
        }
    }
}

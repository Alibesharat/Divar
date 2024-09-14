using divar.ViewModels;
using Newtonsoft.Json;

namespace divar.Services
{
    public class DivarService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public DivarService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }


        public async Task<PostData> GetPostDataAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token must not be null or empty", nameof(token));
            var GetPostDataUrl = $"https://api.divar.ir/v1/open-platform/finder/post/{token}";

            // Set up the request headers
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);

            try
            {
                // Send GET request
                var response = await _httpClient.GetAsync(GetPostDataUrl);
                response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not success

                // Read response content
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize JSON to object
                var postData = JsonConvert.DeserializeObject<PostData>(jsonResponse);

                return postData;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

    }
}
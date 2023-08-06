using Church.ServiceInterfaces;
using Newtonsoft.Json;
using Church.Models;

namespace Church.Services
{
    public class BibleService : IBibleService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.scripture.api.bible/v1/bibles";

        public BibleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("api-key", "fd1c2949f3739e3c9f9060f98459f6b8");
        }

        public async Task<IEnumerable<Bible>> GetBiblesAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var bibles = JsonConvert.DeserializeObject<IEnumerable<Bible>>(content);
            return bibles;
        }

        // Implement other methods as needed
        public async Task<IEnumerable<Bible>> GetVersesAsync(string book, int chapter, int verse)
        {
            // Construct the URL based on the selected book, chapter, and verse
            string url = $"https://api.bible.com/verses/{book}/{chapter}/{verse}";

            // Make the HTTP request
            var response = await _httpClient.GetAsync(url);

            // Deserialize the response
            var verses = JsonConvert.DeserializeObject<IEnumerable<Bible>>(await response.Content.ReadAsStringAsync());

            return verses;
        }
    }

}

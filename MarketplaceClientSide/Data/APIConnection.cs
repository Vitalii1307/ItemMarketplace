using MarketplaceClientSide.Models.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MarketplaceClientSide.Data
{
    public class APIConnection
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public APIConnection(string baseUri)
        {
            _httpClient = new HttpClient();
            _baseUri = baseUri.TrimEnd('/');
        }

        public async Task<List<Auction>> GetAuctions(string endpoint)
        {
            try
            {
                string requestUri = $"{_baseUri}/{endpoint}";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                // Deserialize the JSON response into a list of Auction objects
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve // This is important for handling $ref and $id
                };

                List<Auction> auctions = JsonSerializer.Deserialize<List<Auction>>(responseBody, options);

                return auctions;
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions or log errors here
                Console.WriteLine($"Error occurred while calling the API: {ex.Message}");
                throw;
            }
        }

        public async Task<Auction> GetAuctionById(string endpoint, int id)
        {
            try
            {
                string requestUri = $"{_baseUri}/{endpoint}/{id}";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                // Deserialize the JSON string into an object of the Auction class
                Auction auction = JsonSerializer.Deserialize<Auction>(responseBody);

                return auction;
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions or log errors here
                Console.WriteLine($"Error occurred while calling the API: {ex.Message}");
                throw;
            }
        }
    }
}

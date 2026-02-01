using MAUIMobile.Models;
using System.Net.Http.Json;

namespace MAUIMobile.Services
{
    public class AstroidService
    {
        HttpClient httpClient;
        List<Astroid> Astroids;

        public AstroidService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Astroid>> GetAstroids(DateTime selectedDate, string apiKey = null)
        {
            var endDate = selectedDate.AddDays(7).ToString("yyyy-MM-dd");
            string dateString = selectedDate.ToString("yyyy-MM-dd");
            var key = apiKey ?? "YNHGBy8zsFPEBk3ZanKfUBL4NstyPWRhfkZQ76gw";
            var url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={dateString}&end_date={endDate}&api_key={key}";
            var response = await httpClient.GetAsync(url);

      
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<NeoFeedResponse>();
            var asteroids = data.NearEarthObjects.SelectMany(x => x.Value).ToList();
            return asteroids;

        }
    }
}

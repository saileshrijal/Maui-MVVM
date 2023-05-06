using MonkeyApp.Model;
using MonkeyApp.Services.Interface;
using System.Net.Http.Json;

namespace MonkeyApp.Services
{
    public class MonkeyService : IMonkeyService
    {
        HttpClient _httpClient;
        List<Monkey> monkeyList = new();

        public MonkeyService()
        {
            _httpClient = new HttpClient();
        }

        public Task<Monkey> GetMonkey(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Monkey>> GetMonkeys()
        {
            if (monkeyList?.Count > 0)
                return monkeyList;

            var url = "https://montemagno.com/monkeys.json";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            }
            return monkeyList;
        }
    }
}

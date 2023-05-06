using MonkeyApp.Model;

namespace MonkeyApp.Services.Interface
{
    public interface IMonkeyService
    {
        Task<List<Monkey>> GetMonkeys();
        Task<Monkey> GetMonkey(int id);
    }
}

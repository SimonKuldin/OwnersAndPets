using System.Threading.Tasks;

namespace OwnersAndPets.Interface
{
    public interface IHttpHandler
    {
        Task<string> GetStringAsync(string url);
    }
}

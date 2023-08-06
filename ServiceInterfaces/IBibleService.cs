using Church.Models;

namespace Church.ServiceInterfaces
{
    public interface IBibleService
    {
        Task<IEnumerable<Bible>> GetBiblesAsync();
        Task<IEnumerable<Bible>> GetVersesAsync(string book, int chapter, int verse);
        // Add other methods as needed
    }

}

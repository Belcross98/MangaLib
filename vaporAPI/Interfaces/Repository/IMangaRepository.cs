using vaporAPI.Helpers;
using vaporAPI.Models;

namespace vaporAPI.Interfaces.Repository
{
    public interface IMangaRepository
    {
        Task<List<Manga>> GetAllAsync(QueryObject queryObject);
        Task<Manga?> GetByIdAsync(int id);
        Task<Manga?> CreateAsync(Manga manga);
        Task UpdateAsync();
        Task<Manga?> DeleteAsync(Manga manga);
        Task<Manga?> MangaNameExists(string name);

    }
}
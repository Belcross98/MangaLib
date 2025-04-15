using Microsoft.EntityFrameworkCore;
using vaporAPI.Data;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Repository;
using vaporAPI.Models;

namespace vaporAPI.Repository
{
    public class MangaRepository : IMangaRepository
    {

        private readonly ApplicationDbContext _context;
        public MangaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Manga?> CreateAsync(Manga manga)
        {
            await _context.AddAsync(manga);
            await _context.SaveChangesAsync();
            return manga;
        }


        public async Task<Manga?> DeleteAsync(Manga manga)
        {
            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();
            return manga;
        }

        public async Task<List<Manga>> GetAllAsync(QueryObject queryObject)
        {
            var mangas = _context.Mangas.Include(r => r.Reviews).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.MangaName))
            {
                mangas = mangas.Where(m => m.Name.Contains(queryObject.MangaName));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {

                if (queryObject.SortBy.Equals("MangaName", StringComparison.OrdinalIgnoreCase))
                {

                    mangas = queryObject.IsDescending ? mangas.OrderByDescending(m => m.Name) : mangas.OrderBy(m => m.Name);
                }

            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await mangas.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Manga?> GetByIdAsync(int id)
        {
            return await _context.Mangas.Include(r => r.Reviews).ThenInclude(r => r.User).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Manga?> MangaNameExists(string name)
        {
            return await _context.Mangas.FirstOrDefaultAsync(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vaporAPI.Data;
using vaporAPI.Dtos.Manga;
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
            var check = await _context.Mangas.FirstOrDefaultAsync(m => m.Name == manga.Name);

            if (check != null)
                return null;

            await _context.AddAsync(manga);
            await _context.SaveChangesAsync();
            return manga;
        }

        public async Task<Manga?> DeleteAsync(int id)
        {
            var toBeDeleted = await _context.Mangas.FindAsync(id);

            if (toBeDeleted == null)
                return null;

            _context.Mangas.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
            return toBeDeleted;
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
            return await _context.Mangas.Include(r => r.Reviews).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Manga?> UpdateAsync(int id, UpdateMangaDto? mangaDto)
        {
            var check = await _context.Mangas.FindAsync(id);

            if (check == null)
                return null;

            if (mangaDto != null)
            {
                check.Name = string.IsNullOrEmpty(mangaDto.Name) ? check.Name : mangaDto.Name;
                check.Description = string.IsNullOrEmpty(mangaDto.Description) ? check.Description : mangaDto.Description;
                check.MangaPictureURL = string.IsNullOrEmpty(mangaDto.MangaPictureURL) ? check.MangaPictureURL : mangaDto.MangaPictureURL; ;
            }


            await _context.SaveChangesAsync();
            return check;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vaporAPI.Data;
using vaporAPI.Dtos.Manga;
using vaporAPI.Interfaces;
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

        public async Task<List<Manga>> GetAllAsync()
        {
            return await _context.Mangas.ToListAsync();
        }

        public async Task<Manga?> GetByIdAsync(int id)
        {
            return await _context.Mangas.FindAsync(id);
        }

        public async Task<Manga?> UpdateAsync(int id, UpdateMangaDto mangaDto)
        {
            var check = await _context.Mangas.FindAsync(id);

            if (check == null)
                return null;

            check.Name = mangaDto.Name;
            check.Description = mangaDto.Description;
            check.MangaPictureURL = mangaDto.MangaPictureURL;

            await _context.SaveChangesAsync();
            return check;
        }
    }
}
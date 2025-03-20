using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using vaporAPI.Data;
using vaporAPI.Dtos.Manga;
using vaporAPI.Mappers;
using vaporAPI.Models;

namespace vaporAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MangaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMangas()
        {
            var mangas = await _context.Mangas.ToListAsync();
            var mangasDto = mangas.Select(s => s.ToMangaDto());

            return Ok(mangasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var manga = await _context.Mangas.FindAsync(id);
            if (manga != null)
            {
                return Ok(manga.ToMangaDto());
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] CreateMangaDto createMangaDto)
        {
            if (createMangaDto == null || string.IsNullOrEmpty(createMangaDto.Name))
                return BadRequest("Manga Name is required field!");

            var check = await _context.Mangas.FirstOrDefaultAsync(m => m.Name == createMangaDto.Name);
            if (check != null)
                return BadRequest("That manga already exists in our Database!");

            var mangaToBeAdded = createMangaDto.ToCreateMangaDto();
            await _context.Mangas.AddAsync(mangaToBeAdded);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = mangaToBeAdded.Id }, mangaToBeAdded);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateManga([FromBody] UpdateMangaDto updateMangaDto, [FromRoute] int id)
        {
            var check = await _context.Mangas.FindAsync(id);

            if (check == null)
                return NotFound();

            if (string.IsNullOrEmpty(updateMangaDto.Name))
                return BadRequest("You must enter new name for selected Manga!");

            check.Name = updateMangaDto.Name;
            check.Rating = updateMangaDto.Rating;

            await _context.SaveChangesAsync();

            return Ok(check.ToMangaDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveManga([FromRoute] int id)
        {
            var check = await _context.Mangas.FindAsync(id);
            
            if (check == null)
                return NotFound();

            _context.Mangas.Remove(check);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
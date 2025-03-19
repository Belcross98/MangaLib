using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult getAllMangas()
        {

            var mangas = _context.Mangas.ToList()
            .Select(s => s.ToMangaDto());

            return Ok(mangas);
        }
        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        {

            var manga = _context.Mangas.Find(id);
            if (manga != null)
            {
                return Ok(manga.ToMangaDto());
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult createManga([FromBody] CreateMangaDto createMangaDto)
        {

            System.Console.WriteLine("nisam ni dosao do ovde");
            if (createMangaDto == null || createMangaDto.Name == "")
                return BadRequest("Manga Name is required field!");
            
            var check = _context.Mangas.FirstOrDefault(m => m.Name == createMangaDto.Name);
            if(check != null)
                return BadRequest("That manga already exists in our Database!");
            
            var mangaToBeAdded = createMangaDto.ToCreateMangaDto();
            _context.Mangas.Add(mangaToBeAdded);
            _context.SaveChanges();

            return CreatedAtAction(nameof(getById), new { id = mangaToBeAdded.Id}, mangaToBeAdded);
        }


    }
}
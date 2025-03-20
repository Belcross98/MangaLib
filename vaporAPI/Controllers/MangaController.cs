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
using vaporAPI.Interfaces;
using vaporAPI.Mappers;
using vaporAPI.Models;

namespace vaporAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly IMangaRepository _mangaRepo;

        public MangaController(IMangaRepository mangaRepo)
        {
            _mangaRepo = mangaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMangas()
        {
            var mangas = await _mangaRepo.GetAllAsync();
            var mangasDto = mangas.Select(s => s.ToMangaDto());
            return Ok(mangasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var manga = await _mangaRepo.GetByIdAsync(id);

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

            var mangaToBeAdded = createMangaDto.ToCreateMangaDto();
            var check = await _mangaRepo.CreateAsync(mangaToBeAdded);


            return CreatedAtAction(nameof(GetById), new { id = mangaToBeAdded.Id }, mangaToBeAdded);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateManga([FromBody] UpdateMangaDto updateMangaDto, [FromRoute] int id)
        {
            if (string.IsNullOrEmpty(updateMangaDto.Name))
                return BadRequest("You must enter new name for selected Manga!");

            var check = await _mangaRepo.UpdateAsync(id, updateMangaDto);

            if (check == null)
                return NotFound();

            return Ok(check.ToMangaDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveManga([FromRoute] int id)
        {
            var check = await _mangaRepo.DeleteAsync(id);

            if (check == null)
                return NotFound();

            return NoContent();

        }

    }
}
using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Manga;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Repository;
using vaporAPI.Interfaces.Service;
using vaporAPI.Mappers;

namespace vaporAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly IMangaRepository _mangaRepo;
        private readonly IMangaService _mangaService;

        public MangaController(IMangaRepository mangaRepo, IMangaService mangaService
        )
        {
            _mangaRepo = mangaRepo;
            _mangaService = mangaService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMangas([FromQuery] QueryObject queryObject)
        {

            var response = await _mangaService.GetMangaListAsync(queryObject);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mangaService.GetMangaByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] CreateMangaDto createMangaDto)
        {
            var response = await _mangaService.CreateMangaAsync(createMangaDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateManga([FromBody] UpdateMangaDto updateMangaDto, [FromRoute] int id)
        {
            var response = await _mangaService.UpdateMangaAsync(id, updateMangaDto);
            return StatusCode(response.StatusCode, response);

        }

        //[Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> RemoveManga([FromRoute] int id)
        {
            var response = await _mangaService.DeleteMangaAsync(id);
            return StatusCode(response.StatusCode, response);
        }

    }
}
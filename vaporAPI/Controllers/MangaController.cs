using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Manga;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Repository;
using vaporAPI.Mappers;

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
        public async Task<IActionResult> GetAllMangas([FromQuery] QueryObject queryObject)
        {
            var mangas = await _mangaRepo.GetAllAsync(queryObject);
            var mangasDto = mangas.Select(s => s.ToMangaDto());
            return Ok(new ApiResponse<IEnumerable<MangaDto>>(mangasDto, true, "Retrieved all mangas"));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var manga = await _mangaRepo.GetByIdAsync(id);

            if (manga != null)
            {
                return Ok(new ApiResponse<MangaDto>(manga.ToMangaDto(), true, "Manga found"));
            }

            return StatusCode(404, new ApiResponse<int>(id, false, "Manga with the given id does not exist"));
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] CreateMangaDto createMangaDto)
        {
            if (createMangaDto == null || string.IsNullOrEmpty(createMangaDto.Name))
                return BadRequest(new ApiResponse<CreateMangaDto>(createMangaDto, false, "Manga Name is required field"));

            var mangaToBeAdded = createMangaDto.ToCreateFromDto();
            var check = await _mangaRepo.CreateAsync(mangaToBeAdded);

            if (check == null)
                return BadRequest(new ApiResponse<CreateMangaDto>(createMangaDto, false, "Manga with that name already exists"));

            return StatusCode(201, new ApiResponse<CreateMangaDto>(createMangaDto, true, "Manga successfully created"));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateManga([FromBody] UpdateMangaDto updateMangaDto, [FromRoute] int id)
        {
            var check = await _mangaRepo.UpdateAsync(id, updateMangaDto);

            if (check == null)
                return NotFound(new ApiResponse<int>(id, false, "Manga with the given id does not exist"));

            return Ok(new ApiResponse<MangaDto>(check.ToMangaDto(), true, "Manga successfully updated"));

        }

        //[Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> RemoveManga([FromRoute] int id)
        {
            var check = await _mangaRepo.DeleteAsync(id);

            if (check == null)
                return NotFound(new ApiResponse<int>(id, false, "Manga with the given id does not exist"));

            return StatusCode(204, new ApiResponse<MangaDto>(check.ToMangaDto(), true, "Manga successfully deleted"));
        }

    }
}
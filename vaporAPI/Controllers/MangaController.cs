using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Manga;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Service;

namespace vaporAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly IMangaService _mangaService;

        public MangaController(IMangaService mangaService)
        {
            _mangaService = mangaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMangas([FromQuery] QueryObject queryObject)
        {
            try
            {
                var response = await _mangaService.GetMangaListAsync(queryObject);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _mangaService.GetMangaByIdAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateManga([FromBody] CreateMangaDto createMangaDto)
        {
            try
            {
                var response = await _mangaService.CreateMangaAsync(createMangaDto);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateManga([FromBody] UpdateMangaDto updateMangaDto, [FromRoute] int id)
        {
            try
            {
                var response = await _mangaService.UpdateMangaAsync(id, updateMangaDto);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> RemoveManga([FromRoute] int id)
        {
            try
            {
                var response = await _mangaService.DeleteMangaAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }
        }
    }
}
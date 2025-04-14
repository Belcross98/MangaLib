
using vaporAPI.Dtos.Manga;
using vaporAPI.Helpers;

namespace vaporAPI.Interfaces.Service
{
    public interface IMangaService
    {
        Task<ApiResponse<List<MangaDto>>> GetMangaListAsync(QueryObject queryObject);
        Task<ApiResponse<MangaDto>> GetMangaByIdAsync(int id);
        Task<ApiResponse<MangaDto>> CreateMangaAsync(CreateMangaDto createMangaDto);
        Task<ApiResponse<MangaDto>> UpdateMangaAsync(int id, UpdateMangaDto updateMangaDto);
        Task<ApiResponse<MangaDto>> DeleteMangaAsync(int id);

    }
}
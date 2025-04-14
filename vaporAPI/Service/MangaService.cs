using System.Net;
using vaporAPI.Dtos.Manga;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Repository;
using vaporAPI.Interfaces.Service;
using vaporAPI.Mappers;


namespace vaporAPI.Service
{
    public class MangaService : IMangaService
    {

        private readonly IMangaRepository _mangaRepository;
        public MangaService(IMangaRepository mangaRepository)
        {
            _mangaRepository = mangaRepository;
        }

        public async Task<ApiResponse<MangaDto>> CreateMangaAsync(CreateMangaDto createMangaDto)
        {
            var mangaToBeAdded = createMangaDto.ToCreateFromDto();
            var check = await _mangaRepository.MangaNameExists(mangaToBeAdded.Name);
            if (check != null)
                return new ApiResponse<MangaDto>(mangaToBeAdded.ToMangaDto(), false, "Manga with that name already exists", HttpStatusCode.BadRequest);
            await _mangaRepository.CreateAsync(mangaToBeAdded);
            return new ApiResponse<MangaDto>(mangaToBeAdded.ToMangaDto(), true, "Manga created successfully", HttpStatusCode.Created);


        }

        public async Task<ApiResponse<MangaDto>> DeleteMangaAsync(int id)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);

            if (manga == null)
                return new ApiResponse<MangaDto>(null, false, "Manga not found", HttpStatusCode.NotFound);

            var deletedManga = await _mangaRepository.DeleteAsync(manga);
            return new ApiResponse<MangaDto>(deletedManga.ToMangaDto(), true, "Manga deleted successfully", HttpStatusCode.OK);
        }

        public async Task<ApiResponse<MangaDto>> GetMangaByIdAsync(int id)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);
            if (manga == null)
                return new ApiResponse<MangaDto>(null, false, "Manga not found", HttpStatusCode.NotFound);
            return new ApiResponse<MangaDto>(manga.ToMangaDto(), true, "Manga retrieved successfully", HttpStatusCode.OK);
        }

        public async Task<ApiResponse<List<MangaDto>>> GetMangaListAsync(QueryObject queryObject)
        {
            var mangas = await _mangaRepository.GetAllAsync(queryObject);
            var mangasDto = mangas.Select(s => s.ToMangaDto()).ToList();
            return new ApiResponse<List<MangaDto>>(mangasDto, true, "Retrieved all mangas", HttpStatusCode.OK);
        }

        public async Task<ApiResponse<MangaDto>> UpdateMangaAsync(int id, UpdateMangaDto updateMangaDto)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);
            if (manga == null)
                return new ApiResponse<MangaDto>(null, false, "Manga not found", HttpStatusCode.NotFound);

            manga.Name = string.IsNullOrWhiteSpace(updateMangaDto.Name) ? manga.Name : updateMangaDto.Name;
            manga.Description = string.IsNullOrWhiteSpace(updateMangaDto.Description) ? manga.Description : updateMangaDto.Description;
            manga.MangaPictureURL = string.IsNullOrWhiteSpace(updateMangaDto.MangaPictureURL) ? manga.MangaPictureURL : updateMangaDto.MangaPictureURL;

            await _mangaRepository.UpdateAsync();
            return new ApiResponse<MangaDto>(manga.ToMangaDto(), true, "Manga updated successfully", HttpStatusCode.OK);
        }
    }
}
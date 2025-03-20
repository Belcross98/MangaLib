using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.Manga
{
    public class CreateMangaDto
    {
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string MangaPictureURL { get; set; }  = string.Empty;
    }
}
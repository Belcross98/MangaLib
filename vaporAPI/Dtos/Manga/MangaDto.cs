using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.Manga
{
    public class MangaDto
    {
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string MangaPictureURL { get; set; }  = string.Empty;
    [Column(TypeName ="decimal(2,1)")]
    public decimal? AverageRating { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.Manga
{
    public class UpdateMangaDto
    {
        public required string Name { get; set; }
        [Column(TypeName = "decimal(2,1)")]
        public decimal? Rating { get; set; }

    }
}
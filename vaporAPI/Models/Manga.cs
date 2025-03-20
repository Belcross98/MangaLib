using System.ComponentModel.DataAnnotations.Schema;

namespace vaporAPI.Models
{

public class Manga
{
    
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string MangaPictureURL { get; set; }  = string.Empty;
    [Column(TypeName ="decimal(2,1)")]
    public decimal? AverageRating { get; set; }
    
    public List<Review> Reviews { get; set; } = new List<Review>();


}

}
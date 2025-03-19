using System.ComponentModel.DataAnnotations.Schema;

namespace vaporAPI.Models
{

public class Manga
{
    
    public int Id { get; set; }
    public required string Name { get; set; }
    [Column(TypeName ="decimal(2,1)")]
    public decimal? Rating { get; set; }
    
   


}

}
using Microsoft.EntityFrameworkCore;
using vaporAPI.Models;

namespace vaporAPI.Data
{
    
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }

       public DbSet<Manga> Mangas {get;set;}



    }

}
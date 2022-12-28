using Microsoft.EntityFrameworkCore;

namespace PokemonAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) : base(options)
        {

        }
    }
}

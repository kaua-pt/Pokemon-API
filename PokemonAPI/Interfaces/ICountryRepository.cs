using PokemonAPI.Models;

namespace PokemonAPI.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int OwnerId);
        ICollection<Owner> GetOwnersFromACountry(int id);
        bool CountryExists(int id);
    }
}

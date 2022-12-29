using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PokemonAPI.Dto;
using PokemonAPI.Interfaces;
using PokemonAPI.Models;

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        // convenção, joga na interface e ela já linka com o repository por herança lá no program
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        public PokemonController(IPokemonRepository pokemonRepository,IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons  = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int id)
        { 
            if(!_pokemonRepository.PokemonExists(id))
                return NotFound();
            
            var pokemon = _mapper.Map<Pokemon>(_pokemonRepository.GetPokemon(id));

            // validação por baixo dos panos, verifica se de fato foram passados dados corretos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(Decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int id) 
        {
            if (!_pokemonRepository.PokemonExists(id))
                return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Dto;
using PokemonAPI.Interfaces;
using PokemonAPI.Models;

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper) 
        {
            _category= categoryRepository;
            _mapper= mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Category>))]
        public ActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_category.GetCategories());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public ActionResult GetCategory(int id)
        {
            if (!_category.CategoryExists(id))
                return NotFound();

            var category = _mapper.Map<CategoryDto>( _category.GetCategory(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }


        [HttpGet("pokemon/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public ActionResult GetPokemonByCategory(int id)
        {
            if (!_category.CategoryExists(id))
                return NotFound();

            var pokemons = _category.GetPokemonByCategory(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }
    }
}

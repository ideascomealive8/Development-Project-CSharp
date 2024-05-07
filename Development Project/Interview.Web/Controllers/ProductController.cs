using System.ComponentModel.DataAnnotations;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sparcpoint;
using Sparcpoint.Models;

namespace Interview.Web.Controllers
{
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        //EVAL: normally I would create a level of abstraction between repository and controller
        //but it would be very thin in this case, and is not worth the time
        //omitting it for lack of time
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// returns list of all products
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        
        /// <summary>
        /// Returns a product by Id, or NotFound
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([Required]int id, CancellationToken cancellationToken)
        {
            var result = await _repository.FindByIdAsync(id, cancellationToken);
            return result is not null ? Ok(result) : NotFound();
        }
        
        /// <summary>
        /// Returns a list of product where name contains parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("/name")]
        public async Task<IActionResult> GetByName([Required] string name, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByNameAsync(name, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a list of product filtered by filter (filters are case sensitive) 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("/filter")]
        public async Task<IActionResult> GetByFilter(FilterProduct filter, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByFilterAsync(filter, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new product instance
        /// </summary>
        /// <param name="create"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> CreateProduct([FromBody] Product create, CancellationToken cancellationToken)
        {
            var result = await _repository.AddAsync(create, cancellationToken);
            return Created($"GetById/{result.Id}", result);
        }
    }
}

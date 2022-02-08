using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Domain.Abstractions.Services;
using ETZ.Lending.Presentation.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETZ.Lending.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductDomainService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductDomainService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
        {
            var products = await _service.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return Ok(viewModels);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> SearchProducts([FromQuery] string query)
        {
            var products = await _service.SearchAsync(query);

            var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ProductViewModel>(product);
            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductViewModel>> PutProduct(int id, ProductViewModel product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<Product>(product);
            await _service.UpdateAsync(dto);

            var viewModel = _mapper.Map<ProductViewModel>(dto);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> PostProduct(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = _mapper.Map<Product>(product);
            var created = await _service.CreateAsync(dto);

            var createdViewModel = _mapper.Map<ProductViewModel>(created);
            return CreatedAtAction(nameof(GetProduct), new { id = createdViewModel.Id }, createdViewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
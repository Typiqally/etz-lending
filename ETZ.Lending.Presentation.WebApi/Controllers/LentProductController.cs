using System.Collections.Generic;
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
    public class LentProductController : Controller
    {
        private readonly ILentProductDomainService _service;
        private readonly IMapper _mapper;

        public LentProductController(ILentProductDomainService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LentProductViewModel>>> GetLentProducts()
        {
            var products = await _service.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<LentProductViewModel>>(products);
            return Ok(viewModels);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LentProductViewModel>> GetLentProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<LentProductViewModel>(product);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<LentProductViewModel>> PostLendProduct(LentProductViewModel lentProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = _mapper.Map<LentProduct>(lentProduct);
            var created = await _service.LendProductAsync(dto);

            var createdViewModel = _mapper.Map<LentProductViewModel>(created);
            return CreatedAtAction(nameof(GetLentProduct), new { id = createdViewModel.Id }, createdViewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LentProductViewModel>> DeleteLentProduct(int id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
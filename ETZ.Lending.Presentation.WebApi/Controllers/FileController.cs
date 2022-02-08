using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Domain.Abstractions.Services;
using ETZ.Lending.Presentation.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETZ.Lending.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileDomainService _service;
        private readonly IMapper _mapper;

        public FileController(IFileDomainService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileViewModel>>> GetFiles()
        {
            var files = await _service.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<FileViewModel>>(files);
            return Ok(viewModels);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<FileViewModel>> GetFile(Guid id)
        {
            var file = await _service.GetByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<FileViewModel>(file);
            return Ok(viewModel);
        }

        [HttpGet("{id:Guid}/retrieve")]
        public async Task<ActionResult> RetrieveFile(Guid id)
        {
            var file = await _service.GetByIdAsync(id, true);
            if (file?.Stream == null)
            {
                return NotFound();
            }

            return File(file.Stream, file.ContentType ?? MediaTypeNames.Application.Octet);
        }

        [HttpPost]
        [RequestSizeLimit(1048576)] // 1 MiB
        public async Task<ActionResult<IEnumerable<FileViewModel>>> PostFiles(IEnumerable<IFormFile> files)
        {
            var filesList = files.ToList();

            if (!filesList.Any()) ModelState.AddModelError("File", "No files provided.");
            if (filesList.Count > 10) ModelState.AddModelError("File", "The maximum of 10 files for each request is exceeded.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = new List<File>();
            foreach (var file in filesList)
            {
                list.Add(await _service.UploadAsync(file));
            }

            var viewModels = _mapper.Map<IEnumerable<FileViewModel>>(list);
            return Ok(viewModels);
        }
    }
}
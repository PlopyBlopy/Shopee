using Application.Services;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Core.Contracts.Request;
using Core.Contracts.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _service;
        private readonly IMapper _mapper;

        public CategoryController(CategoryService categoryService, IMapper mapper)
        {
            _service = categoryService;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] CategoryRequest request, CancellationToken ct)
        {
            if (request == null)
            {
                return NoContent();
            }

            Category model = _mapper.Map<Category>(request);

            await _service.Add(model, ct);

            return Ok();
        }

        [HttpPost("AddArray")]
        public async Task<ActionResult> AddArray([FromQuery] IEnumerable<CategoryRequest> requests, CancellationToken ct)
        {
            if (!requests.Any() || requests == null)
            {
                return NoContent();
            }

            IEnumerable<Category> models = requests.Select(r => _mapper.Map<Category>(r));

            await _service.Add(models, ct);

            return Ok();
        }

        [HttpGet("Read_{id:guid}")]
        public async Task<ActionResult<CategoryResponse>> Read(Guid id, CancellationToken ct)
        {
            Category model = await _service.Read(id, ct);
            CategoryResponse response = _mapper.Map<CategoryResponse>(model);

            return Ok(response);
        }

        [HttpGet("ReadArray")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> Read([FromQuery] Guid[] ids, CancellationToken ct)
        {
            if (!ids.Any() || ids == null)
            {
                return NoContent();
            }

            IEnumerable<Category> models = await _service.Read(ids, ct);

            IEnumerable<CategoryResponse> response = models.Select(m => _mapper.Map<CategoryResponse>(m));

            return Ok(response);
        }

        [HttpGet("ReadAll")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>?>> ReadAll(CancellationToken ct)
        {
            IEnumerable<Category> models = await _service.ReadAll(ct);

            IEnumerable<CategoryResponse> response = models.Select(m => _mapper.Map<CategoryResponse>(m));

            return Ok(response);
        }

        [HttpPut("Update_{id:guid}")]
        public async Task<ActionResult<CategoryResponse>> Update(Guid id, [FromBody] CategoryRequest request, CancellationToken ct)
        {
            Category model = _mapper.Map<Category>(request);

            await _service.Update(id, model, ct);

            Category updatedModel = await _service.Read(id, ct);

            return Ok(updatedModel);
        }

        [HttpDelete("Delete{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _service.Delete(id, ct);

            return Ok();
        }

        [HttpDelete("DeleteArray")]
        public async Task<ActionResult> Delete([FromQuery] Guid[] ids, CancellationToken ct)
        {
            await _service.Delete(ids, ct);

            return Ok();
        }
    }
}
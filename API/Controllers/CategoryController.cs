using Application.Services;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Core.Contracts.Request;
using Core.Contracts.Response;
using Core.Contracts.DTO;

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
                return BadRequest();
            }

            Category model;

            try
            {
                Category entity = await _service.Get(request.ParentCategoryId, ct);
                model = _mapper.Map<Category>(request);
            }
            catch (KeyNotFoundException ex)
            {
                model = Category.Create(Guid.NewGuid(), request.Title, Guid.Parse("ffa731f7-28d7-45c4-9039-029c441c54ee")).model;
            }
            //model = Category.Create(Guid.NewGuid(), request.Title, Guid.Parse("27c58ec1-89b8-4903-baa5-b9cf505a2331")).model;

            model = _mapper.Map<Category>(request);
            await _service.Add(model, ct);

            return Ok();
        }

        [HttpPost("AddRange")]
        public async Task<ActionResult> AddARange([FromQuery] IEnumerable<CategoryRequest> requests, CancellationToken ct)
        {
            if (!requests.Any() || requests == null)
            {
                return BadRequest();
            }

            IEnumerable<Category> models = requests.Select(r => _mapper.Map<Category>(r));

            await _service.AddRange(models, ct);

            return Ok();
        }

        [HttpGet("Get{id:guid}")]
        public async Task<ActionResult<CategoryResponse>> Get(Guid id, CancellationToken ct)
        {
            Category model = await _service.Get(id, ct);
            CategoryResponse response = _mapper.Map<CategoryResponse>(model);

            return Ok(response);
        }

        [HttpGet("GetRange")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetRange([FromQuery] Guid[] ids, CancellationToken ct)
        {
            if (!ids.Any() || ids == null)
            {
                return BadRequest();
            }

            IEnumerable<Category> models = await _service.GetRange(ids, ct);

            IEnumerable<CategoryResponse> response = models.Select(m => _mapper.Map<CategoryResponse>(m));

            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>?>> GetAll(CancellationToken ct)
        {
            IEnumerable<Category> models = await _service.GetAll(ct);

            IEnumerable<CategoryResponse> response = models.Select(m => _mapper.Map<CategoryResponse>(m));

            return Ok(response);
        }

        [HttpGet("GetCategoryTree")]
        public async Task<ActionResult<CategoryReadAllDto>> GetCategoryTree(CancellationToken ct)
        {
            CategoryReadAllDto dto = await _service.GetCategoryTree(ct);

            CategoryReadAllResponse response = new CategoryReadAllResponse(dto);

            return Ok(dto);
        }

        [HttpPut("Update{id:guid}")]
        public async Task<ActionResult<CategoryResponse>> Update(Guid id, [FromBody] CategoryRequest request, CancellationToken ct)
        {
            Category model = _mapper.Map<Category>(request);

            await _service.Update(id, model, ct);

            Category updatedModel = await _service.Get(id, ct);

            return Ok(updatedModel);
        }

        [HttpDelete("Delete{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _service.Delete(id, ct);

            return Ok();
        }

        [HttpDelete("DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromQuery] Guid[] ids, CancellationToken ct)
        {
            await _service.DeleteRange(ids, ct);

            return Ok();
        }
    }
}
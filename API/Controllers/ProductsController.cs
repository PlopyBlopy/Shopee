using Application.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Core.Contracts.Request;
using Core.Contracts.Response;
using AutoMapper;
using Core.Contracts.DTO;
using Core.Filters;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(ProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] ProductCreateRequest request, CancellationToken ct)
        {
            if (request == null)
            {
                return NoContent();
            }

            Product model = _mapper.Map<Product>(request);

            await _service.Add(model, ct);

            return Ok();
        }

        [HttpPost("AddRange")]
        public async Task<ActionResult> AddRange([FromQuery] IEnumerable<ProductCreateRequest> requests, CancellationToken ct)
        {
            if (!requests.Any() || requests == null)
            {
                return NoContent();
            }

            IEnumerable<Product> models = requests.Select(r => _mapper.Map<Product>(r));

            await _service.AddRange(models, ct);

            return Ok();
        }

        [HttpGet("Get{id:guid}")]
        public async Task<ActionResult<ProductFullResponse>> Get(Guid id, CancellationToken ct)
        {
            Product model = await _service.Get(id, ct);
            ProductFullResponse response = _mapper.Map<ProductFullResponse>(model);

            return Ok(response);
        }

        [HttpGet("GetRange")]
        public async Task<ActionResult<IEnumerable<ProductFullResponse>>> GetRange([FromQuery] Guid[] ids, CancellationToken ct)
        {
            if (!ids.Any() || ids == null)
            {
                return NoContent();
            }

            IEnumerable<Product> models = await _service.GetRange(ids, ct);

            IEnumerable<ProductFullResponse> response = models.Select(m => _mapper.Map<ProductFullResponse>(m));

            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProductFullResponse>?>> GetAll(CancellationToken ct)
        {
            IEnumerable<Product> models = await _service.GetAll(ct);

            IEnumerable<ProductFullResponse> response = models.Select(m => _mapper.Map<ProductFullResponse>(m));

            return Ok(response);
        }

        [HttpGet("GetCategoryAll")]
        public async Task<ActionResult<IEnumerable<ProductFullResponse>?>> GetCategoryAll([FromQuery] Guid categoryId, CancellationToken ct)
        {
            IEnumerable<Product> models = await _service.GetCategoryAll(categoryId, ct);

            IEnumerable<ProductFullResponse> response = models.Select(m => _mapper.Map<ProductFullResponse>(m));

            return Ok(response);
        }

        [HttpGet("GetFiltered")]
        public async Task<ActionResult<ProductFilteredResponse?>> GetFiltered([FromQuery] ProductFiltersRequest request, CancellationToken ct)
        {
            ProductFiltersDto filter = _mapper.Map<ProductFiltersDto>(request);

            IEnumerable<ProductCardDto>? dtos = await _service.GetFiltered(filter, ct);

            ProductFilteredResponse response = new ProductFilteredResponse(dtos);

            return Ok(dtos);
        }

        [HttpPut("Update{id:guid}")]
        public async Task<ActionResult<ProductFullResponse>> Update(Guid id, [FromBody] ProductCreateRequest request, CancellationToken ct)
        {
            Product model = _mapper.Map<Product>(request);

            await _service.Update(id, model, ct);

            Product updatedModel = await _service.Get(id, ct);

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
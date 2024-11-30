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

        [HttpPost("AddArray")]
        public async Task<ActionResult> AddArray([FromQuery] IEnumerable<ProductCreateRequest> requests, CancellationToken ct)
        {
            if (!requests.Any() || requests == null)
            {
                return NoContent();
            }

            IEnumerable<Product> models = requests.Select(r => _mapper.Map<Product>(r));

            await _service.Add(models, ct);

            return Ok();
        }

        [HttpGet("Read_{id:guid}")]
        public async Task<ActionResult<ProductFullResponse>> Read(Guid id, CancellationToken ct)
        {
            Product model = await _service.Read(id, ct);
            ProductFullResponse response = _mapper.Map<ProductFullResponse>(model);

            return Ok(response);
        }

        [HttpGet("ReadArray")]
        public async Task<ActionResult<IEnumerable<ProductFullResponse>>> Read([FromQuery] Guid[] ids, CancellationToken ct)
        {
            if (!ids.Any() || ids == null)
            {
                return NoContent();
            }

            IEnumerable<Product> models = await _service.Read(ids, ct);

            IEnumerable<ProductFullResponse> response = models.Select(m => _mapper.Map<ProductFullResponse>(m));

            return Ok(response);
        }

        [HttpGet("ReadAll")]
        public async Task<ActionResult<IEnumerable<ProductFullResponse>?>> ReadAll(CancellationToken ct)
        {
            IEnumerable<Product> models = await _service.ReadAll(ct);

            IEnumerable<ProductFullResponse> response = models.Select(m => _mapper.Map<ProductFullResponse>(m));

            return Ok(response);
        }

        [HttpGet("ReadCategoryAll")]
        public async Task<ActionResult<IEnumerable<ProductFullResponse>?>> ReadCategoryAll(Guid categoryId, CancellationToken ct)
        {
            IEnumerable<Product> models = await _service.ReadCategoryAll(categoryId, ct);

            IEnumerable<ProductFullResponse> response = models.Select(m => _mapper.Map<ProductFullResponse>(m));

            return Ok(response);
        }

        [HttpGet("ReadFiltered")]
        public async Task<ActionResult<ProductFilteredResponse?>> ReadFiltered([FromQuery] ProductFiltersRequest request, CancellationToken ct)
        {
            ProductFiltersDto filter = _mapper.Map<ProductFiltersDto>(request);

            //IEnumerable<Product>? models = await _service.ReadFiltered(filter, ct);
            //IEnumerable<ProductCardDto> dtos = models.Select(m => _mapper.Map<ProductCardDto>(m));
            //ProductFilter.Filters(filter, dtos);

            IEnumerable<ProductCardDto>? dtos = await _service.ReadFiltered(filter, ct);

            ProductFilteredResponse response = new ProductFilteredResponse(dtos);

            return Ok(dtos);
        }

        [HttpPut("Update_{id:guid}")]
        public async Task<ActionResult<ProductFullResponse>> Update(Guid id, [FromBody] ProductCreateRequest request, CancellationToken ct)
        {
            Product model = _mapper.Map<Product>(request);

            await _service.Update(id, model, ct);

            Product updatedModel = await _service.Read(id, ct);

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
using Core.Models;
using Core.Interfaces;
using AutoMapper;
using DataBase.Entities;
using Core.Contracts.DTO;
using Core.Contracts.Request;
using System.Linq.Expressions;

namespace Application.Services
{
    public class ProductService
    {
        private readonly IProductsRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductsRepository productsRepository, IMapper mapper)
        {
            _repository = productsRepository;
            _mapper = mapper;
        }

        public async Task Add(Product model, CancellationToken ct)
        {
            ProductEntity entity = _mapper.Map<ProductEntity>(model);

            await _repository.Add(entity, ct);
        }

        public async Task AddRange(IEnumerable<Product> models, CancellationToken ct)
        {
            IEnumerable<ProductEntity> entities = models.Select(m => _mapper.Map<ProductEntity>(m));

            await _repository.AddRange(entities, ct);
        }

        public async Task<Product> Get(Guid id, CancellationToken ct)
        {
            ProductEntity entity = await _repository.Get(id, ct);
            Product model = _mapper.Map<Product>(entity);
            return model;
        }

        public async Task<IEnumerable<Product>> GetRange(Guid[] ids, CancellationToken ct)
        {
            var entities = await _repository.GetRange(ids, ct);

            IEnumerable<Product> models = entities.Select(e => _mapper.Map<Product>(e));

            return models;
        }

        public async Task<IEnumerable<Product>> GetAll(CancellationToken ct)
        {
            IEnumerable<ProductEntity>? entities = await _repository.GetAll(ct);

            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<Product>();
            }

            IEnumerable<Product> models = entities.Select(e => _mapper.Map<Product>(e));

            return models;
        }

        public async Task<IEnumerable<Product>> GetCategoryAll(Guid categoryId, CancellationToken ct)
        {
            IEnumerable<ProductEntity>? entities = await _repository.GetCategoryAll(categoryId, ct);

            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<Product>();
            }

            IEnumerable<Product> models = entities.Select(e => _mapper.Map<Product>(e));

            return models;
        }

        public async Task<IEnumerable<ProductCardDto>?> GetFiltered(ProductFiltersDto filter, CancellationToken ct)
        {
            //IEnumerable<ProductEntity>? entities = await _repository.ReadFiltered(filter, ct);
            //IEnumerable<ProductCardDto> dtos = entities.Select(m => _mapper.Map<ProductCardDto>(m));

            //IEnumerable<Product> models = entities.Select(e => _mapper.Map<Product>(e));

            IEnumerable<ProductCardDto>? dtos = await _repository.GetFiltered(filter, ct);

            return dtos;
        }

        public async Task Update(Guid id, Product model, CancellationToken ct)
        {
            ProductEntity entity = _mapper.Map<ProductEntity>(model);
            await _repository.Update(id, entity, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            await _repository.Delete(id, ct);
        }

        public async Task DeleteRange(Guid[] ids, CancellationToken ct)
        {
            await _repository.DeleteRange(ids, ct);
        }
    }
}
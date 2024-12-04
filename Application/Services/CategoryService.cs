using Application.Helpers;
using AutoMapper;
using Core.Contracts.DTO;
using Core.Interfaces;
using Core.Models;
using DataBase.Entities;

namespace Application.Services
{
    public class CategoryService
    {
        private readonly ICategoriesRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _repository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task Add(Category model, CancellationToken ct)
        {
            CategoryEntity entity = _mapper.Map<CategoryEntity>(model);
            await _repository.Add(entity, ct);
        }

        public async Task AddRange(IEnumerable<Category> models, CancellationToken ct)
        {
            IEnumerable<CategoryEntity> entities = models.Select(m => _mapper.Map<CategoryEntity>(m));

            await _repository.AddRange(entities, ct);
        }

        public async Task<Category> Get(Guid id, CancellationToken ct)
        {
            CategoryEntity entity = await _repository.Get(id, ct);
            Category model = _mapper.Map<Category>(entity);
            return model;
        }

        public async Task<IEnumerable<Category>> GetRange(Guid[] ids, CancellationToken ct)
        {
            var entities = await _repository.GetRange(ids, ct);

            IEnumerable<Category> models = entities.Select(e => _mapper.Map<Category>(e));

            return models;
        }

        public async Task<IEnumerable<Category>> GetAll(CancellationToken ct)
        {
            IEnumerable<CategoryEntity>? entities = await _repository.GetAll(ct);

            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<Category>();
            }

            IEnumerable<Category> models = entities.Select(e => _mapper.Map<Category>(e));

            return models;
        }

        public async Task<CategoryReadAllDto> GetCategoryTree(CancellationToken ct)
        {
            CategoryEntity entity = await _repository.GetCategoryTree(ct);

            CategoryReadAllDto dto = _mapper.Map<CategoryReadAllDto>(entity);

            Builder.BuildSubcategories(entity, dto, _mapper);

            return dto;
        }

        public async Task Update(Guid id, Category model, CancellationToken ct)
        {
            CategoryEntity entity = _mapper.Map<CategoryEntity>(model);
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
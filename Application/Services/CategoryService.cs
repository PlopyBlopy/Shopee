using AutoMapper;
using Core.Interfaces;
using Core.Models;
using DataBase.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public async Task Add(IEnumerable<Category> models, CancellationToken ct)
        {
            IEnumerable<CategoryEntity> entities = models.Select(m => _mapper.Map<CategoryEntity>(m));

            await _repository.Add(entities, ct);
        }

        public async Task<Category> Read(Guid id, CancellationToken ct)
        {
            CategoryEntity entity = await _repository.Read(id, ct);
            Category model = _mapper.Map<Category>(entity);
            return model;
        }

        public async Task<IEnumerable<Category>> Read(Guid[] ids, CancellationToken ct)
        {
            var entities = await _repository.Read(ids, ct);

            IEnumerable<Category> models = entities.Select(e => _mapper.Map<Category>(e));

            return models;
        }

        public async Task<IEnumerable<Category>> ReadAll(CancellationToken ct)
        {
            IEnumerable<CategoryEntity>? entities = await _repository.ReadAll(ct);

            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<Category>();
            }

            IEnumerable<Category> models = entities.Select(e => _mapper.Map<Category>(e));

            return models;
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

        public async Task Delete(Guid[] ids, CancellationToken ct)
        {
            await _repository.Delete(ids, ct);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using DataBase.Entities;
using Core.Interfaces;
using Core.Models;

namespace DataBase.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IProductsServiceDbContext _context;

        public CategoriesRepository(IProductsServiceDbContext context)
        {
            _context = context;
        }

        public async Task Add(CategoryEntity entity, CancellationToken ct)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            CategoryEntity? parentIsThere = await Get(entity.ParentCategoryId, ct);

            if (parentIsThere == null)
                throw new KeyNotFoundException($"The parent category was not found. ParentCategoryId: {entity.ParentCategoryId}");

            await _context.Categories.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task AddRange(IEnumerable<CategoryEntity> entities, CancellationToken ct)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("The provided category collection cannot be null or empty.", nameof(entities));

            await _context.Categories.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<CategoryEntity?> Get(Guid id, CancellationToken ct)
        {
            var entity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
            return entity;
        }

        public async Task<IEnumerable<CategoryEntity>?> GetRange(Guid[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the categories cannot be null or empty.", nameof(ids));

            var entities = await _context.Categories
                    .AsNoTracking()
                    .Where(e => ids.Contains(e.Id))
                    .ToListAsync(ct);

            return entities;
        }

        public async Task<IEnumerable<CategoryEntity>?> GetAll(CancellationToken ct)
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<CategoryEntity> GetCategoryTree(CancellationToken ct)
        {
            var parentCategoryId = Category.DEFAULT_CATEGORY_ID;

            var query = _context.Categories.AsNoTracking().Include(c => c.Subcategories);

            for (int i = 0; i < 4; i++) // Улучшить
            {
                query = query.ThenInclude(c => c.Subcategories);
            }

            var category = await query.FirstOrDefaultAsync(c => c.Id == parentCategoryId, ct);

            return category;
        }

        public async Task Update(Guid id, CategoryEntity entity, CancellationToken ct)
        {
            var oldEntity = await _context.Categories.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");

            oldEntity.Title = entity.Title;
            oldEntity.ParentCategoryId = entity.ParentCategoryId;

            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            var entity = await _context.Categories.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteRange(Guid[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the categories cannot be null or empty.", nameof(ids));

            var entities = await _context.Categories
                .Where(e => ids.Contains(e.Id))
                .ToListAsync(ct);

            var notFoundIds = ids.Except(entities.Select(e => e.Id)).ToArray();

            if (!entities.Any() && notFoundIds.Length > 0)
            {
                throw new KeyNotFoundException($"No categories found with the provided IDs: {string.Join(", ", notFoundIds)}.");
            }

            _context.Categories.RemoveRange(entities);
            await _context.SaveChangesAsync(ct);
        }
    }
}
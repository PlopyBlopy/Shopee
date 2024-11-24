using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;

namespace Persistence.Repositories
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

            await _context.Categories.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Add(IEnumerable<CategoryEntity> entities, CancellationToken ct)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("The provided category collection cannot be null or empty.", nameof(entities));

            await _context.Categories.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<CategoryEntity?> Read(int id, CancellationToken ct)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
        }

        public async Task<IEnumerable<CategoryEntity>?> Read(int[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the categories cannot be null or empty.", nameof(ids));

            var entities = await _context.Categories
                    .AsNoTracking()
                    .Where(e => ids.Contains(e.Id))
                    .ToListAsync(ct);

            if (!entities.Any())
            {
                throw new KeyNotFoundException("No Categories found for the provided IDs.");
            }

            return entities;
        }

        public async Task Update(int id, Action<CategoryEntity> entity, CancellationToken ct)
        {
            var oldEntity = await _context.Categories.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
            entity(oldEntity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(int id, CancellationToken ct)
        {
            var entity = await _context.Categories.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(int[] ids, CancellationToken ct)
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
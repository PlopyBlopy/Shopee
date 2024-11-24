using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IProductsServiceDbContext _context;

        public ProductsRepository(IProductsServiceDbContext context)
        {
            _context = context;
        }

        public async Task Add(ProductEntity entity, CancellationToken ct)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Products.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Add(IEnumerable<ProductEntity> entities, CancellationToken ct)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("The provided product collection cannot be null or empty.", nameof(entities));

            await _context.Products.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ProductEntity?> Read(int id, CancellationToken ct)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, ct) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        public async Task<IEnumerable<ProductEntity>?> Read(int[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the products cannot be null or empty.", nameof(ids));

            var entities = await _context.Products
                    .AsNoTracking()
                    .Where(e => ids.Contains(e.Id))
                    .ToListAsync(ct);

            if (!entities.Any())
            {
                throw new KeyNotFoundException("No products found for the provided IDs.");
            }

            return entities;
        }

        public async Task Update(int id, Action<ProductEntity> entity, CancellationToken ct)
        {
            var oldEntity = await _context.Products.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            entity(oldEntity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(int id, CancellationToken ct)
        {
            var entity = await _context.Products.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(int[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the products cannot be null or empty.", nameof(ids));

            var entities = await _context.Products
                .Where(e => ids.Contains(e.Id))
                .ToListAsync(ct);

            var notFoundIds = ids.Except(entities.Select(e => e.Id)).ToArray();

            if (!entities.Any() && notFoundIds.Length > 0)
            {
                throw new KeyNotFoundException($"No products found with the provided IDs: {string.Join(", ", notFoundIds)}.");
            }

            _context.Products.RemoveRange(entities);
            await _context.SaveChangesAsync(ct);
        }
    }
}
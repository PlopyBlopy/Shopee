using Microsoft.EntityFrameworkCore;
using DataBase.Entities;
using Core.Interfaces;

namespace DataBase.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly IProductsServiceDbContext _context;

        public ImagesRepository(IProductsServiceDbContext context)
        {
            _context = context;
        }

        public async Task Add(ImageEntity entity, CancellationToken ct)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Images.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task AddRange(IEnumerable<ImageEntity> entities, CancellationToken ct)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("The provided image collection cannot be null or empty.", nameof(entities));

            await _context.Images.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ImageEntity?> Get(Guid id, CancellationToken ct)
        {
            var entity = await _context.Images
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, ct) ?? throw new KeyNotFoundException($"Image with ID {id} not found.");
            return entity;
        }

        public async Task<IEnumerable<ImageEntity>?> GetRange(Guid[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the images cannot be null or empty.", nameof(ids));

            var entities = await _context.Images
                    .AsNoTracking()
                    .Where(e => ids.Contains(e.Id))
                    .ToListAsync(ct);

            return entities;
        }

        public async Task<IEnumerable<ImageEntity>?> GetAll(CancellationToken ct)
        {
            return await _context.Images
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task Update(Guid id, ImageEntity entity, CancellationToken ct)
        {
            var oldEntity = await _context.Images.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");

            oldEntity.Order = entity.Order;
            oldEntity.Path = entity.Path;
            oldEntity.ProductId = entity.ProductId;

            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            var entity = await _context.Images.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Image with ID {id} not found.");
            _context.Images.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteRange(Guid[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the images cannot be null or empty.", nameof(ids));

            var entities = await _context.Images
                .Where(e => ids.Contains(e.Id))
                .ToListAsync(ct);

            var notFoundIds = ids.Except(entities.Select(e => e.Id)).ToArray();

            if (!entities.Any() && notFoundIds.Length > 0)
            {
                throw new KeyNotFoundException($"No images found with the provided IDs: {string.Join(", ", notFoundIds)}.");
            }

            _context.Images.RemoveRange(entities);
            await _context.SaveChangesAsync(ct);
        }
    }
}
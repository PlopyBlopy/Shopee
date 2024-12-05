using Microsoft.EntityFrameworkCore;
using DataBase.Entities;
using Core.Interfaces;
using Core.Contracts.DTO;
using Core.Filters;
using Core.Models;

namespace DataBase.Repositories
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

        public async Task AddRange(IEnumerable<ProductEntity> entities, CancellationToken ct)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("The provided product collection cannot be null or empty.", nameof(entities));

            await _context.Products.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ProductEntity?> Get(Guid id, CancellationToken ct)
        {
            var entity = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            return entity;
        }

        public async Task<IEnumerable<ProductEntity>?> GetRange(Guid[] ids, CancellationToken ct)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("The identifiers specified for the products cannot be null or empty.", nameof(ids));

            var entities = await _context.Products
                    .AsNoTracking()
                    .Where(e => ids.Contains(e.Id))
                    .ToListAsync(ct);

            return entities;
        }

        public async Task<IEnumerable<ProductEntity>?> GetAll(CancellationToken ct)
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<ProductEntity>?> GetCategoryAll(Guid categoryId, CancellationToken ct)
        {
            if (categoryId == Category.DEFAULT_CATEGORY_ID)
            {
                return await _context.Products
                .AsNoTracking()
                .ToListAsync(ct);
            }

            return await _context.Products
                .AsNoTracking()
                .Where(e => e.CategoryId == categoryId)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<ProductCardDto>?> GetFiltered(ProductFiltersDto filter, CancellationToken ct)
        {
            IQueryable<ProductEntity> queryEnities = _context.Products
                .AsNoTracking()
                .Where(e => string.IsNullOrWhiteSpace(filter.Search) ||
                            e.Title.ToLower().Contains(filter.Search.ToLower()));

            if (filter.CategoryId != Category.DEFAULT_CATEGORY_ID)
            {
                queryEnities = queryEnities.Where(e => e.CategoryId == filter.CategoryId);
            }

            ProductFilter.Filters(filter, ref queryEnities);

            IEnumerable<ProductCardDto> dtos = await queryEnities
                .Select(e => new ProductCardDto(e.Id, e.Title, e.Price, e.Rating))
                .ToListAsync(ct);

            return dtos;
        }

        public async Task Update(Guid id, ProductEntity entity, CancellationToken ct)
        {
            var oldEntity = await _context.Products.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");

            oldEntity.Title = entity.Title;
            oldEntity.Description = entity.Description;
            oldEntity.Price = entity.Price;
            oldEntity.Rating = entity.Rating;
            oldEntity.CreateAt = entity.CreateAt;
            oldEntity.SellerId = entity.SellerId;
            oldEntity.CategoryId = entity.CategoryId;

            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            var entity = await _context.Products.FindAsync(id, ct) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteRange(Guid[] ids, CancellationToken ct)
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
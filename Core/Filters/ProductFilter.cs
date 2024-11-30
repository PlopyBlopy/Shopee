using Core.Contracts.DTO;
using DataBase.Entities;
using System.Linq.Expressions;

namespace Core.Filters
{
    public class ProductFilter
    {
        public static void Filters(ProductFiltersDto filter, ref IQueryable<ProductEntity> entities)
        {
            Expression<Func<ProductEntity, object>> selectorKey = GetSelectorKey(filter.SortProp?.ToLower());

            entities = filter.SortOrder == "desc"
                ? entities.OrderByDescending(selectorKey)
                : entities.OrderBy(selectorKey);
        }

        private static Expression<Func<ProductEntity, object>> GetSelectorKey(string sortProp)
        {
            switch (sortProp)
            {
                case "title":
                    return product => product.Title;

                case "price":
                    return product => product.Price;

                case "rating":
                    return product => product.Rating;

                default:
                    return product => product.Id;
            }
        }
    }
}
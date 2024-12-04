using AutoMapper;
using Core.Contracts.DTO;
using DataBase.Entities;

namespace Application.Helpers
{
    internal class Builder
    {
        public static void BuildSubcategories(CategoryEntity parentCategory, CategoryReadAllDto parentDto, IMapper mapper)
        {
            if (parentCategory.Subcategories != null)
            {
                foreach (var subCategory in parentCategory.Subcategories)
                {
                    // Создаем DTO для подкатегории
                    CategoryReadAllDto subDto = mapper.Map<CategoryReadAllDto>(subCategory);
                    // Добавляем его в список подкатегорий родителя
                    parentDto.Subcategories.Add(subDto);
                    // Рекурсивно строим его подкатегории
                    BuildSubcategories(subCategory, subDto, mapper);
                }
            }
        }
    }
}
using AutoMapper;
using Core.Contracts.Request;
using Core.Contracts.Response;
using Core.Models;
using DataBase.Entities;

namespace Core.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequest, Category>()
            .ConvertUsing(src => SrcRequestDestModel(src));
            CreateMap<Category, CategoryResponse>()
            .ConvertUsing(src => SrcModelDestResponse(src));

            CreateMap<CategoryEntity, Category>()
            .ConvertUsing(src => SrcEntityDestModel(src));
            CreateMap<Category, CategoryEntity>()
            .ConvertUsing(src => SrcModelDestEntity(src));
        }

        private Category SrcRequestDestModel(CategoryRequest src)
        {
            Category model = Category.Create(Guid.NewGuid(), src.Title, src.ParentCategoryId).model;
            return model;
        }

        private CategoryResponse SrcModelDestResponse(Category src)
        {
            CategoryResponse response = new CategoryResponse(src.Id, src.Title, src.ParentCategoryId);
            return response;
        }

        private CategoryEntity SrcModelDestEntity(Category src)
        {
            CategoryEntity entity = new CategoryEntity()
            {
                Id = src.Id,
                Title = src.Title,
                ParentCategoryId = src.ParentCategoryId,
            };
            return entity;
        }

        private Category SrcEntityDestModel(CategoryEntity src)
        {
            Category model = Category.Create(src.Id, src.Title, src.ParentCategoryId).model;
            return model;
        }
    }
}
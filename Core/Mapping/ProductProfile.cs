using AutoMapper;
using Core.Contracts.DTO;
using Core.Contracts.Request;
using Core.Contracts.Response;
using Core.Models;
using DataBase.Entities;

namespace Core.Mapping
{
    public class ProductProfile : Profile
    {
        //private readonly IMapper _mapper;

        public ProductProfile()
        {
            CreateMap<ProductCreateRequest, Product>()
            .ConvertUsing(src => SrcRequestDestModel(src));
            CreateMap<Product, ProductFullResponse>()
            .ConvertUsing(src => SrcModelDestResponse(src));

            CreateMap<Product, ProductEntity>()
            .ConvertUsing(src => SrcModelDestEntity(src));
            CreateMap<ProductEntity, Product>()
            .ConvertUsing(src => SrcEntityDestModel(src));

            CreateMap<ProductFiltersRequest, ProductFiltersDto>()
            .ConstructUsing(src => SrcRequestDestDto(src));
            CreateMap<Product, ProductCardDto>()
            .ConstructUsing(src => SrcModelDestCardDto(src));
            CreateMap<ProductEntity, ProductCardDto>()
            .ConstructUsing(src => SrcEntityDestCardDto(src));
        }

        //public ProductProfile(IMapper mapper)
        //{
        //    _mapper = mapper;

        //    CreateMap<ProductCreateRequest, Product>()
        //    .ConvertUsing(src => SrcRequestDestModel(src));
        //    CreateMap<Product, ProductFullResponse>()
        //    .ConvertUsing(src => SrcModelDestResponse(src));

        //    CreateMap<Product, ProductEntity>()
        //    .ConvertUsing(src => SrcModelDestEntity(src));
        //    CreateMap<ProductEntity, Product>()
        //    .ConvertUsing(src => SrcEntityDestModel(src));
        //}

        private Product SrcRequestDestModel(ProductCreateRequest src)
        {
            Product model = Product.Create(
                Guid.NewGuid(), src.Title, src.Description, src.Price, src.Rating, DateTime.UtcNow, src.SellerId, src.CategoryId).model;
            return model;
        }

        private ProductFullResponse SrcModelDestResponse(Product src)
        {
            ProductFullResponse response = new ProductFullResponse(src.Id, src.Title, src.Description, src.Price, src.Rating, src.CreateAt, src.SellerId, src.CategoryId);
            return response;
        }

        private ProductEntity SrcModelDestEntity(Product src)
        {
            ProductEntity entity = new ProductEntity()
            {
                Id = src.Id,
                Title = src.Title,
                Description = src.Description,
                Price = src.Price,
                Rating = src.Rating,
                CreateAt = src.CreateAt,
                SellerId = src.SellerId,
                CategoryId = src.CategoryId,
            };
            return entity;
        }

        private Product SrcEntityDestModel(ProductEntity src)
        {
            Product model = Product.Create(src.Id, src.Title, src.Description, src.Price, src.Rating, src.CreateAt, src.SellerId, src.CategoryId).model;
            return model;
        }

        private ProductFiltersDto SrcRequestDestDto(ProductFiltersRequest src)
        {
            ProductFiltersDto dto = new ProductFiltersDto(src?.Search, src?.SortItem, src?.SortOrder);
            return dto;
        }

        private ProductCardDto SrcModelDestCardDto(Product src)
        {
            ProductCardDto dto = new ProductCardDto(src.Id, src.Title, src.Price, src.Rating);
            return dto;
        }

        private ProductCardDto SrcEntityDestCardDto(ProductEntity src)
        {
            ProductCardDto dto = new ProductCardDto(src.Id, src.Title, src.Price, src.Rating);
            return dto;
        }
    }
}
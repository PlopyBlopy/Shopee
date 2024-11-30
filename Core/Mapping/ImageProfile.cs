using AutoMapper;
using Core.Contracts.Request;
using Core.Contracts.Response;
using Core.Models;
using DataBase.Entities;

namespace Core.Mapping
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageRequest, Image>()
            .ConvertUsing(src => SrcRequestDestModel(src));
            CreateMap<Image, ImageResponse>()
            .ConvertUsing(src => SrcModelDestResponse(src));

            CreateMap<Image, ImageEntity>()
            .ConvertUsing(src => SrcModelDestEntity(src));
            CreateMap<ImageEntity, Image>()
            .ConvertUsing(src => SrcEntityDestModel(src));
        }

        private Image SrcRequestDestModel(ImageRequest src)
        {
            string path = string.Empty;
            Image model = Image.Create(Guid.NewGuid(), src.Order, path, src.ProductId).model;
            return model;
        }

        private ImageResponse SrcModelDestResponse(Image src)
        {
            ImageResponse response = new ImageResponse(src.Id, src.Order, src.ProductId);
            return response;
        }

        private ImageEntity SrcModelDestEntity(Image src)
        {
            ImageEntity entity = new ImageEntity()
            {
                Id = src.Id,
                Order = src.Order,
                Path = src.Path,
                ProductId = src.ProductId,
            };
            return entity;
        }

        private Image SrcEntityDestModel(ImageEntity src)
        {
            Image model = Image.Create(src.Id, src.Order, src.Path, src.ProductId).model;
            return model;
        }
    }
}
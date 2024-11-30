using AutoMapper;
using Core.Interfaces;
using Core.Models;
using DataBase.Entities;

namespace Application.Services
{
    public class ImageService
    {
        private readonly IImagesRepository _repository;
        private readonly IMapper _mapper;

        public ImageService(IImagesRepository imagesRepository, IMapper mapper)
        {
            _repository = imagesRepository;
            _mapper = mapper;
        }

        public async Task Add(Image model, CancellationToken ct)
        {
            ImageEntity entity = _mapper.Map<ImageEntity>(model);
            await _repository.Add(entity, ct);
        }

        public async Task Add(IEnumerable<Image> models, CancellationToken ct)
        {
            IEnumerable<ImageEntity> entities = models.Select(m => _mapper.Map<ImageEntity>(m));

            await _repository.Add(entities, ct);
        }

        public async Task<Image> Read(Guid id, CancellationToken ct)
        {
            ImageEntity entity = await _repository.Read(id, ct);
            Image model = _mapper.Map<Image>(entity);
            return model;
        }

        public async Task<IEnumerable<Image>> Read(Guid[] ids, CancellationToken ct)
        {
            var entities = await _repository.Read(ids, ct);

            IEnumerable<Image> models = entities.Select(e => _mapper.Map<Image>(e));

            return models;
        }

        public async Task<IEnumerable<Image>> ReadAll(CancellationToken ct)
        {
            IEnumerable<ImageEntity>? entities = await _repository.ReadAll(ct);

            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<Image>();
            }

            IEnumerable<Image> models = entities.Select(e => _mapper.Map<Image>(e));

            return models;
        }

        public async Task Update(Guid id, Image model, CancellationToken ct)
        {
            ImageEntity entity = _mapper.Map<ImageEntity>(model);
            await _repository.Update(id, entity, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            await _repository.Delete(id, ct);
        }

        public async Task Delete(Guid[] ids, CancellationToken ct)
        {
            await _repository.Delete(ids, ct);
        }
    }
}
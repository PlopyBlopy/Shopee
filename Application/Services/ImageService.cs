﻿using AutoMapper;
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

        public async Task AddRange(IEnumerable<Image> models, CancellationToken ct)
        {
            IEnumerable<ImageEntity> entities = models.Select(m => _mapper.Map<ImageEntity>(m));

            await _repository.AddRange(entities, ct);
        }

        public async Task<Image> Get(Guid id, CancellationToken ct)
        {
            ImageEntity entity = await _repository.Get(id, ct);
            Image model = _mapper.Map<Image>(entity);
            return model;
        }

        public async Task<IEnumerable<Image>> GetRange(Guid[] ids, CancellationToken ct)
        {
            var entities = await _repository.GetRange(ids, ct);

            IEnumerable<Image> models = entities.Select(e => _mapper.Map<Image>(e));

            return models;
        }

        public async Task<IEnumerable<Image>> GetAll(CancellationToken ct)
        {
            IEnumerable<ImageEntity>? entities = await _repository.GetAll(ct);

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

        public async Task DeleteRange(Guid[] ids, CancellationToken ct)
        {
            await _repository.DeleteRange(ids, ct);
        }
    }
}
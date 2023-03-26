﻿using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class EngineService : IEngineService
	{
        private readonly IRepository<EngineDataModel> _engineRepository;

        public EngineService(IRepository<EngineDataModel> engineRepository)
        {
            _engineRepository = engineRepository;
        }

        public IEngine GetEngineModel(int id)
        {
            var engineDataModel = GetEngine(id);

            if (engineDataModel == null)
            {
                throw new EngineNameNotFoundException();
            }

            var engineViewModel = new EngineDomainModel(engineDataModel, _engineRepository);
            return engineViewModel;
        }

        public async Task<IEnumerable<IEngine>> GetEngineModelsAsync()
        {
            var engineEntities = await _engineRepository
                .GetAllAsync(
                include: query => query
                .Include(e => e.Brand)
                .Include(e => e.Model)
                .Include(e => e.Generation)
                );

            if (engineEntities == null)
            {
                throw new EngineNameNotFoundException();
            }

            var engineViewModels = engineEntities
                .Select(i => new EngineDomainModel()
                {
                    EngineId = i.EngineId,
                    IdentificationName= i.IdentificationName,
                    Type = i.Type,
                    Capacity = i.Capacity,
                    Power = i.Power,
                    Description = i.Description,
                    BrandId = i.BrandId,
                    Brand = i.Brand.Name.ToString(),
					ModelId = i.ModelId,
                    Model = i.Model.Name.ToString(),
                    GenerationId = i.GenerationId,
                    Generation = i.Generation.Name.ToString(),
				}).ToList();
            var engineModels = engineViewModels.Cast<IEngine>();
            return engineModels;
        }
 
        public IEngine CreateEngineDomainModel()
        {
            var engine = new EngineDomainModel(_engineRepository);
            return engine;
        }

        public EngineDataModel GetEngine(int engineId)
        {
            var engine = _engineRepository
                .Get(
                    predicate: bt => bt.EngineId == engineId, include: query => query
                        .Include(g => g.Brand)
                        .Include(g => g.Model)
                        .Include(g => g.Generation)
                );

            if (engine == null)
            {
                throw new EngineNameNotFoundException();
            }
            return engine;
        }
    }
}
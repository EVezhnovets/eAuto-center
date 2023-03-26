﻿using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Domain.Services
{
    public sealed class CarService : ICarService
	{
        private readonly IRepository<CarDataModel> _carRepository;

        public CarService(IRepository<CarDataModel> carRepository)
        {
            _carRepository = carRepository;
        }

        public ICar GetCarModel(int id)
        {
            var carDataModel = GetCar(id);

            if (carDataModel == null)
            {
                throw new CarNotFoundException();
            }

            var carViewModel = new CarDomainModel(carDataModel, _carRepository);
            return carViewModel;
        }

        public async Task<IEnumerable<ICar>> GetCarModelsAsync()
        {
            var carEntities = await _carRepository
                .GetAllAsync(
                include: query => query
                .Include(e => e.Brand)
                .Include(e => e.Model)
                .Include(e => e.Generation)
                .Include(e => e.BodyType)
                .Include(e => e.Engine)
                .Include(e => e.DriveType)
                .Include(e => e.Transmission)
                );

            if (carEntities == null)
            {
                throw new CarNotFoundException();
            }

            var carViewModels = carEntities
                .Select(i => new CarDomainModel()
                {
                    CarId = i.CarId,
                    PriceInitial = i.PriceInitial,
                    PictureUrl = i.PictureUrl,
                    Year = i.Year,
                    DateArrival = i.DateArrival,
                    Odometer = i.Odometer,
                    Description = i.Description,
                    BrandId = i.BrandId,
                    Brand = i.Brand.Name.ToString(),
                    ModelId = i.ModelId,
                    Model = i.Model.Name.ToString(),
                    GenerationId = i.GenerationId,
                    Generation = i.Generation.Name.ToString(),
                    BodyTypeId = i.BodyTypeId,
                    BodyType = i.BodyType.Name.ToString(),
                    EngineId = i.EngineId,
                    Engine = i.Engine.IdentificationName.ToString(),
                    DriveTypeId = i.DriveTypeId,
                    DriveType = i.DriveType.Name.ToString(),
                    TransmissionId = i.TransmissionId,
                    Transmission = i.Transmission.Name.ToString()
                }).ToList();
            var carModels = carViewModels.Cast<ICar>();
            return carModels;
        }
 
        public ICar CreateCarDomainModel()
        {
            var car = new CarDomainModel(_carRepository);
            return car;
        }

        public CarDataModel GetCar(int carId)
        {
            var car = _carRepository
                .Get(
                    predicate: bt => bt.CarId == carId, include: query => query
                        .Include(g => g.Brand)
                        .Include(g => g.Model)
                        .Include(g => g.Generation)
                        .Include(g => g.BodyType)
                        .Include(g => g.Engine)
                        .Include(g => g.DriveType)
                        .Include(g => g.Transmission)
                );

            if (car == null)
            {
                throw new CarNotFoundException();
            }
            return car;
        }
    }
}
﻿using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace eAuto.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class CarsCatalogController : Controller
	{
		private readonly IAppLogger<CarsCatalogController> _logger;

		private readonly ICarService _carService;
		private readonly IBrandService _brandService;
		private readonly IModelService _modelService;
		private readonly IGenerationService _generationService;
		private readonly IBodyTypeService _bodyTypeService;
		private readonly IDriveTypeService _driveTypeService;
		private readonly ITransmissionService _transmissionService;
		private readonly IEngineTypeService _engineTypeService;

		public CarsCatalogController(
			ICarService carService,
			IBrandService brandService,
			IModelService modelService,
			IGenerationService generationService,
			IBodyTypeService bodyTypeService,
			IDriveTypeService driveTypeService,
			ITransmissionService transmissionService,
			IEngineTypeService engineTypeService,
			IAppLogger<CarsCatalogController> logger)
		{
			_carService = carService;
			_brandService = brandService;
			_modelService = modelService;
			_generationService = generationService;
			_bodyTypeService = bodyTypeService;
			_driveTypeService = driveTypeService;
			_transmissionService = transmissionService;
			_engineTypeService = engineTypeService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Index(CarsIndexViewModel carsIndex, int? pageId)
		{
			try
			{
				var iCarsList = await _carService.GetCarModelsAsync();
				var carsList = iCarsList
					.Select(i => new CarViewModel
					{
						CarId = i.CarId,
						PriceInitial = i.PriceInitial,
						PictureUrl = i.PictureUrl,
						Year = i.Year,
						DateArrival = i.DateArrival,
						Odometer = i.Odometer,
						Description = i.Description,
						BrandId = i.BrandId,
						EngineIdentificationName = i.EngineIdentificationName,
						EngineCapacity = i.EngineCapacity,
						EngineFuelType = i.EngineFuelType,
						EngineFuelTypeId = i.EngineFuelTypeId,
						EnginePower = i.EnginePower,
						Brand = i.Brand,
						ModelId = i.ModelId,
						Model = i.Model,
						GenerationId = i.GenerationId,
						Generation = i.Generation,
						BodyTypeId = i.BodyTypeId,
						BodyType = i.BodyType,
						DriveTypeId = i.DriveTypeId,
						DriveType = i.DriveType,
						TransmissionId = i.TransmissionId,
						Transmission = i.Transmission
					});
				var carsQuery = carsList
					.Where(i => (!carsIndex.BrandFilterApplied.HasValue || i.BrandId == carsIndex.BrandFilterApplied)
								&& (!carsIndex.ModelFilterApplied.HasValue || i.ModelId == carsIndex.ModelFilterApplied)
								&& (!carsIndex.GenerationFilterApplied.HasValue || i.GenerationId == carsIndex.GenerationFilterApplied)
								&& (!carsIndex.BodyTypeFilterApplied.HasValue || i.BodyTypeId == carsIndex.BodyTypeFilterApplied)
								&& (!carsIndex.DriveTypeFilterApplied.HasValue || i.DriveTypeId == carsIndex.DriveTypeFilterApplied)
								&& (!carsIndex.TransmissionFilterApplied.HasValue || i.TransmissionId == carsIndex.TransmissionFilterApplied)
								&& (!carsIndex.EngineTypeFilterApplied.HasValue || i.EngineFuelTypeId == carsIndex.EngineTypeFilterApplied))
					.Select(i =>
						new CarViewModel(
							i.CarId,
							i.PriceInitial,
							i.PictureUrl,
							i.Year,
							i.DateArrival,
							i.Odometer,
							i.Description,
							i.EngineIdentificationName,
							i.EngineCapacity,
							i.EngineFuelType,
							i.EngineFuelTypeId,
							i.EnginePower,
							i.Brand,
							i.Model,
							i.Generation,
							i.BodyType,
							i.DriveType,
							i.Transmission
					)).ToList();

				var carsResult = new CarsIndexViewModel()
				{
					CarVModels = carsQuery,
					Brands = new List<SelectListItem>(),
					Models = new List<SelectListItem>(),
					Generations = new List<SelectListItem>(),
					BodyTypes = new List<SelectListItem>(),
					DriveTypes = new List<SelectListItem>(),
					Transmissions = new List<SelectListItem>(),
					EngineTypes = new List<SelectListItem>()

				};

				return View(carsResult);
			}
			catch (GenericNotFoundException<CarsCatalogController> ex)
			{
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}

		}

		public IActionResult Details(int id)
		{
			var queryCar = _carService.GetCarModel(id);

			var result = new CarViewModel(
                    id,
                    queryCar.PriceInitial,
                    queryCar.PictureUrl,
                    queryCar.Year,
                    queryCar.DateArrival,
                    queryCar.Odometer,
                    queryCar.Description,
                    queryCar.EngineIdentificationName,
                    queryCar.EngineCapacity,
                    queryCar.EngineFuelType,
                    queryCar.EngineFuelTypeId,
                    queryCar.EnginePower,
                    queryCar.Brand,
                    queryCar.Model,
                    queryCar.Generation,
                    queryCar.BodyType,
                    queryCar.DriveType,
                    queryCar.Transmission);
       
			return View(result);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		#region API
		[HttpGet]
		public async Task<ActionResult> GetBrands()
		{
			var brands = await _brandService.GetBrandModelsAsync();
			var query = brands
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.BrandId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetBodyTypes()
		{
			var bodyTypes = await _bodyTypeService.GetBodyTypeModelsAsync();
			var query = bodyTypes
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.BodyTypeId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetDriveTypes()
		{
			var driveTypes = await _driveTypeService.GetDriveTypeModelsAsync();
			var query = driveTypes
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.DriveTypeId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetTransmissions()
		{
			var transmissions = await _transmissionService.GetTransmissionModelsAsync();
			var query = transmissions
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.TransmissionId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetEngineTypes()
		{
			var engineTypes = await _engineTypeService.GetEngineTypesAsync();
			var query = engineTypes
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.EngineTypeId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetModels(int BrandFilterApplied)
		{
			var modelsSel = await _modelService.GetModelModelsAsync();
			var query = modelsSel
				.Where(m => m.BrandId == BrandFilterApplied)
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.ModelId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetGenerations(int ModelFilterApplied)
		{
			var generationsSel = await _generationService.GetGenerationModelsAsync();
			var query = generationsSel
				.Where(m => m.ModelId == ModelFilterApplied)
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.GenerationId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}
		#endregion
	}
}
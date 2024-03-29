﻿using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdminRole)]
    public class TransmissionController : Controller
    {
        private readonly IAppLogger<TransmissionController> _logger;
        private readonly ITransmissionService _transmissionService;

        public TransmissionController(
            ITransmissionService transmissionService ,IAppLogger<TransmissionController> logger)
        {
            _logger = logger;
            _transmissionService = transmissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var transmissions = await _transmissionService.GetTransmissionModelsAsync();
                var viewModelList = transmissions
                    .Select(i => new TransmissionViewModel
                    {
                        TransmissionId = i.TransmissionId,
                        Name = i.Name
                    })
                    .OrderBy(i => i.Name);
				return View(viewModelList);
			}
			catch (GenericNotFoundException<TransmissionController> ex)
			{
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
            
        }

		#region Create

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(TransmissionViewModel viewModel)
		{
			ITransmission transmission;
			try
			{
                if (ModelState.IsValid)
                {
                    transmission = _transmissionService.CreateTransmissionDomainModel();

                    transmission.TransmissionId = viewModel.TransmissionId;
                    transmission.Name = viewModel.Name;

                    transmission.Save();
                    TempData["Success"] = "Transmission created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (GenericNotFoundException<TransmissionController> ex)
			{
				_logger.LogError(ex, ex.Message);
				return RedirectToAction("Index");
			}
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var viewModel = _transmissionService.GetTransmissionModel(id);

                var transmissionViewModel = new TransmissionViewModel
                {
                    TransmissionId = id,
                    Name = viewModel.Name
                };
                return View(transmissionViewModel);
            }

            catch (GenericNotFoundException<TransmissionController> ex)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TransmissionViewModel viewModel)
        {
            ITransmission transmission;
            try
            {
                if (ModelState.IsValid)
                {
                    transmission = _transmissionService.GetTransmissionModel(viewModel.TransmissionId);
                    transmission.Name = viewModel.Name;
                    transmission.Save();
                    TempData["Success"] = "Transmission edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (GenericNotFoundException<TransmissionController> ex)
            {
				_logger.LogError(ex, ex.Message);
				return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var transmission = _transmissionService.GetTransmissionModel(id);
                transmission.Delete();
                TempData["Success"] = "Transmission deleted successfully";
                return RedirectToAction("Index");
            }

            catch (GenericNotFoundException<TransmissionController> ex)
            {
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
            }
            #endregion
        }
    }
}
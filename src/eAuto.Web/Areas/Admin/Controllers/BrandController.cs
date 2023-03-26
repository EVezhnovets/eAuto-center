﻿using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService ,ILogger<BrandController> logger)
        {
            _logger = logger;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var result = await _brandService.GetBrandModelsAsync();
				return View(result);
			}
			catch (BrandNotFoundException ex)
			{
				//TODO logger
				return NotFound(ex.Message);
			}
			catch(Exception ex)
			{
				//TODO logger
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
		public IActionResult Create(BrandViewModel viewModel)
		{
			IBrand brand;
			try
			{
                if (ModelState.IsValid)
                {
                    brand = _brandService.CreateBrandModel(viewModel.Name);
                    brand.Save();
                    TempData["Success"] = "Brand created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (BrandNotFoundException)
			{
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
                var viewModel = _brandService.GetBrandModel(id);

                var brandViewModel = new BrandViewModel
                {
                    BrandId = id,
                    Name = viewModel.Name
                };
                return View(brandViewModel);
            }

            catch (BrandNotFoundException ex)
            {
                _logger!.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger!.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BrandViewModel viewModel)
        {
            IBrand brand;
            try
            {
                if (ModelState.IsValid)
                {
                    brand = _brandService.GetBrandModel(viewModel.BrandId);
                    brand.Name = viewModel.Name;
                    brand.Save();
                    TempData["Success"] = "Brand edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (BrandNotFoundException)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var brand = _brandService.GetBrandModel(id);
                brand.Delete();
                TempData["Success"] = "Brand deleted successfully";
                return RedirectToAction("Index");
            }

            catch (BrandNotFoundException ex)
            {
                _logger!.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger!.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            #endregion
        }
    }
}
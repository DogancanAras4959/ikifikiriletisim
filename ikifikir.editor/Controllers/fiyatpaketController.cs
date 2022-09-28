using AutoMapper;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentTypeData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingData;
using ikifikir.CORE.Helper.Extends;
using ikifikir.editor.Models.PricingDataModel.PricingComponentModel;
using ikifikir.editor.Models.PricingDataModel.PricingComponentTypeModel;
using ikifikir.editor.Models.PricingDataModel.PricingModel;
using ikifikir.ENGINES.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Controllers
{
    public class fiyatpaketController : Controller
    {

        #region Constructur

        private readonly IMapper _mapper;
        private readonly IPricingService _pricingService;
        public fiyatpaketController(IMapper mapper, IPricingService pricingService)
        {
            _mapper = mapper;
            _pricingService = pricingService;
        }

        #endregion

        #region Pricing

        [HttpGet]
        [Authorize]
        public IActionResult fiyatpaketleri(int? pageNumber)
        {
            int pageSize = 50;
            List<PricingListViewModel> values = _mapper.Map<List<PricingListItemDto>, List<PricingListViewModel>>(_pricingService.getPricingList());
            return View(PaginationList<PricingListViewModel>.Create(values.ToList(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        [Authorize]
        public IActionResult fiyatpaketEkle()
        {
            return View(new PricingCreateViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> fiyatpaketOlustur(PricingCreateViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.Image = SaveFileProcess.ImageInsert(file, "Admin");
                }
                else
                {
                    model.Image = "bg.jpg";
                }

                if (await _pricingService.insertPricing(_mapper.Map<PricingCreateViewModel, PricingDto>(model)))
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
                else
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
            }
            else
            {
                return RedirectToAction("fiyatpaketleri", "fiyatpaket");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult fiyatpaketGuncelle(int Id)
        {
            var value = _mapper.Map<PricingDto, PricingEditViewModel>(_pricingService.getPricingById(Id));
            return View(value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> fiyatpaketiGuncellestir(PricingEditViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.Image = SaveFileProcess.ImageInsert(file, "Admin");
                }

                if (await _pricingService.updatePricing(_mapper.Map<PricingEditViewModel, PricingDto>(model)))
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
                else
                    return RedirectToAction("fiyatpaketGuncelle", "fiyatpaket", new { Id = model.Id });
            }
            else
            {
                return RedirectToAction("fiyatpaketGuncelle", "fiyatpaket", new { Id = model.Id });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult fiyatpaketdetay(int Id)
        {
            var value = _mapper.Map<PricingDto, PricingEditViewModel>(_pricingService.getPricingById(Id));

            ViewBag.PaketBilesenleri = _mapper.Map<List<PricingComponentListItemDto>, List<PricingComponentListViewModel>>(_pricingService.getPricingComponentListByPricePackageId(value.Id));

            return View(value);
        }

        [HttpGet]
        [Authorize]
        public IActionResult paketSil(int Id)
        {
            if (_pricingService.deletePricing(Id))
                return RedirectToAction("fiyatpaketleri", "fiyatpaket");
            else
                return RedirectToAction("fiyatpaketleri", "fiyatpaket");
        }

        #endregion

        #region PricingComponents

        [HttpGet]
        [Authorize]
        public IActionResult paketBileseniEkle(int Id)
        {
            PricingComponentCreateViewModel model = new PricingComponentCreateViewModel();
            model.PricingId = Id;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> paketBileseniOlustur(PricingComponentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _pricingService.insertPricingComponent(_mapper.Map<PricingComponentCreateViewModel, PricingComponentDto>(model)))
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
                else
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
            }
            else
            {
                return RedirectToAction("fiyatpaketleri", "fiyatpaket");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult paketBileseniGuncelle(int Id)
        {
            var value = _mapper.Map<PricingComponentDto, PricingComponentViewModel>(_pricingService.getPricingComponentById(Id));
            return View(value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> paketBilesenleriDuzenle(PricingComponentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var component = _mapper.Map<PricingComponentDto, PricingComponentViewModel>(_pricingService.getPricingComponentById(model.Id));

                model.PricingId = component.PricingId;

                if (await _pricingService.updatePricingComponent(_mapper.Map<PricingComponentEditViewModel, PricingComponentDto>(model)))
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
                else
                    return RedirectToAction("fiyatpaketGuncelle", "fiyatpaket", new { Id = model.Id });
            }
            else
            {
                return RedirectToAction("fiyatpaketGuncelle", "fiyatpaket", new { Id = model.Id });
            }
        }

        #endregion

        #region PricingComponentTypes

        [HttpGet]
        [Authorize]
        public IActionResult paketTipleri(int Id)
        {
            var value = _mapper.Map<PricingComponentDto, PricingComponentViewModel>(_pricingService.getPricingComponentById(Id));

            ViewBag.BilesenTipleri = _mapper.Map<List<PricingComponentTypeListItemDto>, List<PricingComponentTypeListViewModel>>(_pricingService.getPricingComponentTypeByTypeId(value.Id));

            return View(value);
        }

        [HttpGet]
        [Authorize]
        public IActionResult paketTipleriEkle(int Id)
        {
            PricingComponentTypeCreateViewModel model = new PricingComponentTypeCreateViewModel();
            model.pricingComponentId = Id;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> paketTipleriOlustur(PricingComponentTypeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _pricingService.insertPricingComponentTypes(_mapper.Map<PricingComponentTypeCreateViewModel, PricingComponentTypeDto>(model)))
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
                else
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
            }
            else
            {
                return RedirectToAction("fiyatpaketleri", "fiyatpaket");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult paketTipleriGuncelle(int Id)
        {
            var value = _mapper.Map<PricingComponentTypeDto, PricingComponentTypeEditViewModel>(_pricingService.getPricingComponentTypeById(Id));
            return View(value);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> paketTipleriDuzenle(PricingComponentTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _pricingService.updatePricingComponentType(_mapper.Map<PricingComponentTypeEditViewModel, PricingComponentTypeDto>(model)))
                    return RedirectToAction("fiyatpaketleri", "fiyatpaket");
                else
                    return RedirectToAction("paketTipleriGuncelle", "fiyatpaket", new { Id = model.Id });
            }
            else
            {
                return RedirectToAction("paketTipleriGuncelle", "fiyatpaket", new { Id = model.Id });
            }
        }

        #endregion

    }
}

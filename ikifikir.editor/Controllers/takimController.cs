using AutoMapper;
using ikifikir.COMMON.DataTransfer.TeamsData;
using ikifikir.CORE.Helper.Extends;
using ikifikir.editor.Models.TeamsModel;
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
    public class takimController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamService;
        public takimController(IMapper mapper, ITeamsService teamService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
        }

        [Authorize]
        public IActionResult ekipler()
        {
            var values = _mapper.Map<List<TeamsListItemDto>, List<TeamsListViewModel>>(_teamService.getTeamList());
            return View(values);
        }

        [Authorize]
        public IActionResult personelekle()
        {
            return View(new TeamsCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> personelolustur(TeamsCreateViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                        model.image = SaveFileProcess.ImageInsert(file, "Admin");

                    else
                        model.image = "user.png";

                    if (await _teamService.insertTeam(_mapper.Map<TeamsCreateViewModel, TeamsDto>(model)))
                        return RedirectToAction(nameof(ekipler));

                    else
                        return RedirectToAction(nameof(ekipler));
                }
                else
                {
                    return RedirectToAction(nameof(ekipler));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(ekipler));
            }
        }

        [Authorize]
        public IActionResult personelduzenle(int id)
        {
            try
            {
                var getTeam = _mapper.Map<TeamsDto, TeamsEditViewModel>(_teamService.getTeamById(id));
                return View(getTeam);
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("anasayfa", "yonetim");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> personelguncelle(TeamsEditViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getTeam = _mapper.Map<TeamsDto, TeamsEditViewModel>(_teamService.getTeamById(model.Id));

                    if (file != null)
                        model.image = SaveFileProcess.ImageInsert(file, "Admin");
                    else
                        model.image = getTeam.image;

                    if (await _teamService.updateTeam(_mapper.Map<TeamsEditViewModel, TeamsDto>(model)))
                        return RedirectToAction(nameof(ekipler));

                    else
                        return View(model);
                }
                else
                {
                    return RedirectToAction(nameof(ekipler));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(ekipler));
            }
        }

        [Authorize]
        public IActionResult personelsil(int id)
        {
            if (!_teamService.deleteTeam(id))
            {
                return RedirectToAction(nameof(ekipler));
            }
            else
            {
                return RedirectToAction(nameof(ekipler));
            }
        }
    }
}

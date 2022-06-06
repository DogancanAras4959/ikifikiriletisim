using AutoMapper;
using ikifikir.COMMON.DataTransfer.CategoryData;
using ikifikir.COMMON.DataTransfer.ProjectData;
using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
using ikifikir.COMMON.DataTransfer.TagProjectData;
using ikifikir.COMMON.DataTransfer.TeamsData;
using ikifikir.COMMON.DataTransfer.VideoData;
using ikifikir.CORE.EmailConfig;
using ikifikir.ENGINES.Interface;
using ikifikirweb.Models.MetaConfig;
using ikifikirweb.Models.reCaptchaConfig;
using ikifikirweb.ViewModels.CategoryModel;
using ikifikirweb.ViewModels.EmailModel;
using ikifikirweb.ViewModels.GalleryModel;
using ikifikirweb.ViewModels.ProjectModel;
using ikifikirweb.ViewModels.TagProjectModel;
using ikifikirweb.ViewModels.TeamModel;
using ikifikirweb.ViewModels.VideoModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.Controllers
{
    public class anasayfaController : Controller
    {

        #region Constructures

        private readonly IMapper _mapper;
        private readonly reCaptchaService _repService;
        private readonly IProjectService _projectService;
        private readonly ITeamsService _teamService;
        private readonly IEmailSender _emailSender;
        private readonly ICategoryService _categoryService;
        private readonly IGalleryService _galleryService;
        private readonly IVideoService _videoService;
        public anasayfaController(reCaptchaService repService, IMapper mapper, IProjectService projectService, ITeamsService teamService, IEmailSender emailSender, ICategoryService categoryService, IGalleryService galleryService, IVideoService videoService)
        {
            _repService = repService;
            _mapper = mapper;
            _projectService = projectService;
            _teamService = teamService;
            _emailSender = emailSender;
            _categoryService = categoryService;
            _galleryService = galleryService;
            _videoService = videoService;
        }

        #endregion

        #region Primary

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult sayfa()
        {
            TempData["footer-light"] = 1;
            TempData["isTransparent"] = 1;
            return View();
        }

        [HttpGet("calisma/{Id}/{projectName}", Name = "calisma")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult calisma(int Id, string projectName)
        {

            TempData["isTransparent"] = 0;

            var value = _mapper.Map<ProjectDto, ProjectViewModel>(_projectService.getProjectById(Id));          
            string friendlyTitle = projectName;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = value.seoTitle;
            //meta.Keywords = newsGet.Tag;
            meta.Description = value.projectSpot;
            meta.Image = "https://uploads.ikifikir.net/images/" + value.imageThumbnail;
            meta.ogDescription = value.projectSpot;
            meta.ogTitle = value.projectName;
            meta.ogImage = "https://uploads.ikifikir.net/images/" + value.imageThumbnail;
            meta.Url = "https://ikifikir.net/" + Id + value.projectName;
            ViewBag.Meta = meta;

            #endregion

            List<GalleryListViewModel> galleries = _mapper.Map<List<GalleryListItemDto>, List<GalleryListViewModel>>(_galleryService.getGalleryImageListByProjectId(value.Id));

            ViewBag.GalleryList = galleries;

            List<VideoListViewModel> videos = _mapper.Map<List<VideoListItemDto>, List<VideoListViewModel>>(_videoService.getGalleryVideoListByProjectId(value.Id));

            ViewBag.Videos = videos;

            List<ProjectListViewModel> projects;
            projects = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectListByCategoryId(value.categoryId));

            ViewBag.MoreProjects = projects;

            #region Tags

            List<TagProjectListViewModel> tags = _mapper.Map<List<TagProjectListItemDto>, List<TagProjectListViewModel>>(_projectService.tagsListWithProjectByProjectId(value.Id));

            ViewBag.tagList = tags;

            #endregion

            if (!string.Equals(friendlyTitle, projectName, StringComparison.Ordinal))
            {
                return this.RedirectToRoutePermanent("calisma", new { id = Id, title = friendlyTitle });
            }
            return View(value);
        }

        [Route("/anasayfa/hata/{code:int}")]
        public IActionResult hata(int code)
        {
            ViewData["ErrorCode"] = $"{code}";
            return View("~/Views/anasayfa/hata.cshtml");
        }

        public IActionResult kategori(int categoryId)
        {
            TempData["isTransparent"] = 0;
            var category = _mapper.Map<CategoryDto, CategoryViewModel>(_categoryService.getCategoryById(categoryId));

            List<ProjectListViewModel> values;
            values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectListByCategoryId(categoryId));

            ViewBag.Projects = values;

            return View(category);
        }

        #endregion

        #region Secondary
        public IActionResult ekibimiz()
        {
            TempData["isTransparent"] = 1;
            List<TeamListViewModel> values;

            values = _mapper.Map<List<TeamsListItemDto>, List<TeamListViewModel>>(_teamService.getTeamList());
            return View(values);
        }

        public IActionResult iletisim()
        {
            TempData["isTransparent"] = 1;
            return View();
        }

        public IActionResult hizmetler()
        {
            TempData["isTransparent"] = 1;
            return View();
        } 

        public IActionResult calismalar(int categoryId = 0)
        {
            TempData["isTransparent"] = 1;
            List<ProjectListViewModel> values;
            
            var listCategories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());

            ViewBag.Categories = listCategories;

            if (categoryId == 0)
            {              
                values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectList());
            }

            else
            {
                values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectListByCategoryId(categoryId));
            }

            return View(values);
        }

        #endregion

        #region External

        [HttpPost]
        public async Task<IActionResult> FormGonderIletisim(EmailSenderViewModel model)
        {
            try
            {
                string messages = model.content;
                var message = new Message()
                {
                    To = "info@ikifikir.net",
                    Subject = model.subject,
                    Phone = "",
                    Email = model.email,
                    GuestCount = "",
                    NameSurname = model.namesurname,
                    Content = $@"<p>{model.namesurname} iletişim formunu doldurdu<p> <hr/> <p>Email Adresi: {model.email}</p> <hr/> <p>{messages}</p> <hr/> <p>Telefon: {model.phone}</p>",
                };

                await _emailSender.SendEmailAsync(message);

                return RedirectToAction(nameof(iletisim));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(iletisim));
            }

        }

        #endregion
    }
}

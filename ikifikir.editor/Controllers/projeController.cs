using AutoMapper;
using ikifikir.COMMON.DataTransfer.CategoryData;
using ikifikir.COMMON.DataTransfer.ProjectData;
using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
using ikifikir.COMMON.DataTransfer.ReferenceData;
using ikifikir.COMMON.DataTransfer.TagProjectData;
using ikifikir.COMMON.DataTransfer.VideoData;
using ikifikir.CORE.Helper.Extends;
using ikifikir.editor.Models.CategoryModel;
using ikifikir.editor.Models.GalleryModel;
using ikifikir.editor.Models.ProjectModel;
using ikifikir.editor.Models.ReferenceLogoModel;
using ikifikir.editor.Models.TagProjectModel;
using ikifikir.editor.Models.VideoModel;
using ikifikir.ENGINES.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Controllers
{
    public class projeController : Controller
    {
        #region Constructures

        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IProjectService _projectService;
        private readonly IGalleryService _galleryService;
        private readonly IVideoService _videoService;

        public projeController(IMapper mapper, ICategoryService categoryService, IProjectService projectService, IGalleryService galleryService, IVideoService videoService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _galleryService = galleryService ?? throw new ArgumentNullException(nameof(galleryService));
            _videoService = videoService ?? throw new ArgumentNullException(nameof(videoService));
        }

        #endregion

        #region Kategori Metotları

        [Authorize]
        public IActionResult kategoriler()
        {
            var values = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());
            return View(values);
        }

        [Authorize]
        public IActionResult kategoriekle()
        {
            return View(new CategoryCreateViewModel());
        }

        [Authorize]
        public IActionResult kategoriduzenle(int id)
        {
            var value = _mapper.Map<CategoryDto, CategoryEditViewModel>(_categoryService.getCategoryById(id));
            return View(value);
        }

        [Authorize]
        public IActionResult kategorisil(int id)
        {
            if (_categoryService.deleteCategory(id))
            {
                return RedirectToAction(nameof(kategoriler));
            }
            else
            {
                return RedirectToAction(nameof(kategoriler));
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> kategoriolustur(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _categoryService.insertCategory(_mapper.Map<CategoryCreateViewModel, CategoryDto>(model)))
                    return RedirectToAction(nameof(kategoriler));
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> kategoriguncelle(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _categoryService.updateCategory(_mapper.Map<CategoryEditViewModel, CategoryDto>(model)))
                    return RedirectToAction(nameof(kategoriler));
            }
            return View();
        }

        #endregion

        #region Proje Metotları

        [Authorize]
        public IActionResult projeler(int? pageNumber, string searchstring, int CategoryId = 0)
        {
            int pageSize = 50;
            List<ProjectListViewModel> values;

            var categories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());

            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "name");

            if (searchstring != "" && searchstring != null)
            {
                values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.searchDataInProject(searchstring));

                return View(PaginationList<ProjectListViewModel>.Create(values.ToList(), pageNumber ?? 1, pageSize));
            }

            if (CategoryId != 0)
            {
                values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectListByCategoryId(CategoryId));

                return View(PaginationList<ProjectListViewModel>.Create(values.ToList(), pageNumber ?? 1, pageSize));
            }

            values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectList());
            return View(PaginationList<ProjectListViewModel>.Create(values.ToList(), pageNumber ?? 1, pageSize));
        }

        [Authorize]
        public IActionResult projeekle()
        {
            var categories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());

            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "name");

            var projectParent = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectParent());

            ViewBag.ProjectParents = new SelectList(projectParent.ToList(), "Id", "projectName");

            return View(new ProjectCreateViewModel());
        }

        [Authorize]
        public IActionResult projeduzenle(int id)
        {
            var categories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());

            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "name");

            var projectParent = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectParent());

            ViewBag.ProjectParents = new SelectList(projectParent.ToList(), "Id", "projectName");

            var value = _mapper.Map<ProjectDto, ProjectEditViewModel>(_projectService.getProjectById(id));

            return View(value);
        }

        [Authorize]
        public IActionResult projesil(int id)
        {
            if (_projectService.deleteProject(id))
            {
                return RedirectToAction(nameof(projeler));
            }
            return RedirectToAction(nameof(projeler));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> projeguncellestir(ProjectEditViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var value = _mapper.Map<ProjectDto, ProjectEditViewModel>(_projectService.getProjectById(model.Id));
                if (file != null)
                {
                    model.imageThumbnail = SaveFileProcess.ImageInsert(file, "Admin");
                }
                else
                {
                    model.imageThumbnail = value.imageThumbnail;
                }

                if (await _projectService.updateProject(_mapper.Map<ProjectEditViewModel, ProjectDto>(model)))
                {
                    return RedirectToAction(nameof(projeler));
                }
            }

            return RedirectToAction(nameof(projeler));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> projeolustur(ProjectCreateViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.imageThumbnail = SaveFileProcess.ImageInsert(file, "Admin");
                    
                }
                else
                {
                    model.imageThumbnail = "user.png";
                }

                if (await _projectService.insertProject(_mapper.Map<ProjectCreateViewModel, ProjectDto>(model)))
                {
                    
                    return RedirectToAction(nameof(projeler));
                }

            }
            return RedirectToAction(nameof(projeler));
        }

        [Authorize]
        public async Task<IActionResult> projebaslikgoster(int id)
        {
            if (await _projectService.projectTitleShowProcess(id))
            {
                return RedirectToAction(nameof(projeler));
            }
            return RedirectToAction(nameof(projeler));
        }

        [Authorize]
        public async Task<IActionResult> projeaktiflestir(int id)
        {
            if (await _projectService.projectIsActiveProcess(id))
            {
                return RedirectToAction(nameof(projeler));
            }
            return RedirectToAction(nameof(projeler));
        }

        [Authorize]
        public IActionResult projedetay(int id)
        {
            var categories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());

            var projectParent = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectParent());

            var galleries = _mapper.Map<List<GalleryListItemDto>, List<GalleryListViewModel>>(_galleryService.getGalleryImageListByProjectId(id));

            var videos = _mapper.Map<List<VideoListItemDto>, List<VideoListViewModel>>(_videoService.getGalleryVideoListByProjectId(id));

            ViewBag.ProjectParents = new SelectList(projectParent.ToList(), "Id", "projectName");
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "name");
            ViewBag.Galleries = galleries;
            ViewBag.Videos = videos;

            var value = _mapper.Map<ProjectDto, ProjectEditViewModel>(_projectService.getProjectById(id));

            #region GetTags

            List<TagProjectListViewModel> tags = _mapper.Map<List<TagProjectListItemDto>, List<TagProjectListViewModel>>(_projectService.tagsListWithProjectByProjectId(value.Id));

            List<string> list = new List<string>();

            foreach (var item in tags)
            {
                list.Add(item.tagToTag.name);
            }

            string[] tagsList = list.ToArray();

            for (int i = 0; i < tagsList.Count(); i++)
            {
                if (value.tagList != null)
                {
                    value.tagList = value.tagList + "," + tagsList[i];
                }
                else
                {
                    value.tagList = tagsList[i];
                }
            }

            #endregion

            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> fotograflariyukleyin(int Id, List<IFormFile> file)
        {
            var value = _mapper.Map<ProjectDto, ProjectEditViewModel>(_projectService.getProjectById(Id));
            value.galleryList = new List<GalleryViewModel>();

            long size = file.Sum(f => f.Length);

            if (file.Count != 0)
            {
                foreach (var formFile in file)
                {
                    var gallery = new GalleryViewModel()
                    {
                        slug = SaveFileProcess.ImageInsert(formFile, "Admin"),
                        sorted = 0,
                        projectId = value.Id,
                    };
                    value.galleryList.Add(gallery);
                }

                bool result = await _galleryService.insertGalleryImage(_mapper.Map<List<GalleryViewModel>, List<GalleryDto>>(value.galleryList));

                return RedirectToAction("projedetay", "proje", new { Id = value.Id });
            }
            else
            {
                return RedirectToAction("projedetay", "proje", new { Id = value.Id });
            }

        }

        public IActionResult fotografisil(int Id)
        {
            var value = _mapper.Map<GalleryDto, GalleryEditViewModel>(_galleryService.getGalleryById(Id));

            if (_galleryService.deleteGalleryImage(Id))
            {
                return RedirectToAction("projedetay", "proje", new { Id = value.projectId });
            }
            return RedirectToAction("projedetay", "proje", new { Id = value.projectId });
        }

        [HttpPost]
        public async Task<IActionResult> etiketekle(string tags, ProjectViewModel model)
        {

            if (!string.IsNullOrEmpty(tags))
            {
                if (tags[^1] == ',')
                {
                    await _projectService.InsertTagToProject(tags[0..^1], model.Id);
                }
                else
                {
                    await _projectService.InsertTagToProject(tags, model.Id);
                }
            }

            return RedirectToAction("projedetay","proje", new { Id = model.Id  });
        }

        #endregion

        #region Referens Logolar

        [Authorize]
        public IActionResult referanslogolar(int? pagenumber)
        {
            int pageSize = 40;
            List<ReferenceLogoListViewModel> values;
            values = _mapper.Map<List<ReferenceListItemDto>,List<ReferenceLogoListViewModel>> (_projectService.referenceLogos());
            return View(PaginationList<ReferenceLogoListViewModel>.Create(values.ToList(), pagenumber ?? 1, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> logolariekleyin(List<IFormFile> file)
        {
            try
            {
                long size = file.Sum(f => f.Length);

                if (file.Count != 0)
                {
                    List<ReferenceLogoListViewModel> model = new List<ReferenceLogoListViewModel>();
                    foreach (var formFile in file)
                    {
                        var gallery = new ReferenceLogoListViewModel()
                        {
                            slug = SaveFileProcess.ImageInsert(formFile, "Admin"),
                            sorted = 0,
                        };
                        model.Add(gallery);
                    }

                    bool result = await _projectService.insertReferenceLogoImage(_mapper.Map<List<ReferenceLogoListViewModel>, List<ReferenceListItemDto>>(model));

                    return RedirectToAction("referanslogolar", "proje");
                }
                else
                {
                    return RedirectToAction("referanslogolar", "proje");
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("referanslogolar", "proje");
            }
            

        }

        #endregion

        #region Video Metotları

        [Authorize]
        public IActionResult media(int? pageNumber)
        {
            int pageSize = 50;
            List<VideoListViewModel> videos;
            videos = _mapper.Map<List<VideoListItemDto>, List<VideoListViewModel>>(_videoService.getVideoListMedia());
            return View(PaginationList<VideoListViewModel>.Create(videos.ToList(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> videoekle(ProjectViewModel project, string urlName, string iframe, IFormFile fileupload)
        {
            VideoCreateViewModel model;

            if (fileupload != null)
            {
                model = new VideoCreateViewModel
                {
                    slug = SaveFileProcess.VideoInsert(fileupload, "Videos"),
                    name = fileupload.FileName,
                    projectId = project.Id,
                    iframe = iframe,
                };
         
            }
            else
            {
                model = new VideoCreateViewModel
                {
                    slug = urlName,
                    iframe = iframe,
                    name = urlName,
                    projectId = project.Id,
                };
            }

            bool result = await _videoService.insertVideo(_mapper.Map<VideoCreateViewModel, VideoDto>(model));

            if (result) return RedirectToAction("projedetay","proje", new { Id = project.Id });
            
            else return RedirectToAction("projedetay", "proje", new { Id = project.Id });

        }

        #endregion
    }
}

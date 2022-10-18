using AutoMapper;
using ikifikir.COMMON.DataTransfer.CategoryData;
using ikifikir.COMMON.DataTransfer.PostData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentTypeData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingData;
using ikifikir.COMMON.DataTransfer.ProjectData;
using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
using ikifikir.COMMON.DataTransfer.ReferenceData;
using ikifikir.COMMON.DataTransfer.TagProjectData;
using ikifikir.COMMON.DataTransfer.TeamsData;
using ikifikir.COMMON.DataTransfer.VideoData;
using ikifikir.CORE.EmailConfig;
using ikifikir.ENGINES.Interface;
using ikifikirweb.Helpers;
using ikifikirweb.Models;
using ikifikirweb.Models.MetaConfig;
using ikifikirweb.Models.reCaptchaConfig;
using ikifikirweb.ViewModels.CategoryModel;
using ikifikirweb.ViewModels.EmailModel;
using ikifikirweb.ViewModels.GalleryModel;
using ikifikirweb.ViewModels.PostModel;
using ikifikirweb.ViewModels.PricingDataModelWeb.PricingComponentTypeWebModel;
using ikifikirweb.ViewModels.PricingDataModelWeb.PricingComponentWebModel;
using ikifikirweb.ViewModels.PricingDataModelWeb.PricingWebModel;
using ikifikirweb.ViewModels.ProjectModel;
using ikifikirweb.ViewModels.ReferenceModel;
using ikifikirweb.ViewModels.TagProjectModel;
using ikifikirweb.ViewModels.TeamModel;
using ikifikirweb.ViewModels.VideoModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
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
        private readonly IPricingService _pricingService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IViewRenderService _viewRenderService; 
        private readonly IPostService _postService;

        public anasayfaController(reCaptchaService repService, IMapper mapper, IProjectService projectService, ITeamsService teamService, IEmailSender emailSender, ICategoryService categoryService, IGalleryService galleryService, IVideoService videoService, IPricingService pricingService, IWebHostEnvironment webHostEnvironment, IViewRenderService viewRenderService, IPostService postService)
        {
            _repService = repService;
            _mapper = mapper;
            _postService = postService;
            _projectService = projectService;
            _teamService = teamService;
            _emailSender = emailSender;
            _categoryService = categoryService;
            _galleryService = galleryService;
            _videoService = videoService;
            _pricingService = pricingService;
            _viewRenderService = viewRenderService;
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        #region Primary

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult sayfa()
        {
            TempData["footer-light"] = 1;
            TempData["isTransparent"] = 1;
            TempData["isHaveFooter"] = 1;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "İkifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion

            var posts = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.getPostListByNotification());
            ViewBag.Post = posts;

            return View();
        }

        [HttpGet("calisma/{Id}/{projectName}", Name = "calisma")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult calisma(int Id, string projectName)
        {

            TempData["isTransparent"] = 0;
            TempData["isHaveFooter"] = 1;

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
            TempData["isHaveFooter"] = 1;

            return View("~/Views/anasayfa/hata.cshtml");
        }
        public IActionResult kategori(int categoryId)
        {
            TempData["isTransparent"] = 0;
            TempData["isHaveFooter"] = 1;

            var category = _mapper.Map<CategoryDto, CategoryViewModel>(_categoryService.getCategoryById(categoryId));

            //char[] delimiterChars = { ',' };

            //string[] words = category.categoryTags.Split(delimiterChars);

            //List<string> newTags = new List<string>();

            //foreach (var item in words)
            //{
            //    newTags.Add(item.Trim());
            //}

            //category.tagsListToCategory = newTags;

            List<ProjectListViewModel> values;
            values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectListByCategoryIdWeb(categoryId));

            ViewBag.Projects = values;

            #region Meta


            #endregion

            return View(category);
        }

        #endregion

        #region Secondary
        public IActionResult ekibimiz()
        {
            TempData["isTransparent"] = 1;
            TempData["isHaveFooter"] = 1;

            List<TeamListViewModel> values;

            values = _mapper.Map<List<TeamsListItemDto>, List<TeamListViewModel>>(_teamService.getTeamList());

            List<ReferenceListViewModel> references;

            references = _mapper.Map<List<ReferenceListItemDto>, List<ReferenceListViewModel>>(_projectService.referenceLogos());

            ViewBag.Reference = references;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion

            return View(values);
        }

        public IActionResult iletisim()
        {
            TempData["isTransparent"] = 0;
            TempData["isHaveFooter"] = 1;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion


            return View();
        }

        public IActionResult hizmetler()
        {
            TempData["isTransparent"] = 1;
            TempData["isHaveFooter"] = 1;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult calismalar(int categoryId = 0)
        {
            TempData["isTransparent"] = 1;
            TempData["isHaveFooter"] = 1;

            List<ProjectListViewModel> values;

            var listCategories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());

            ViewBag.Categories = listCategories;

            if (categoryId == 0)
            {
                values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectListWeb());
            }

            else
            {
                values = _mapper.Map<List<ProjectListItemDto>, List<ProjectListViewModel>>(_projectService.getProjectListByCategoryId(categoryId));
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion

            return View(values);
        }

        public IActionResult isebakisimiz()
        {
            TempData["isTransparent"] = 1;
            TempData["isHaveFooter"] = 1;
            return View();
        }
        public IActionResult notifView()
        {
            return PartialView("notifView");
        }
        #endregion

        #region External

        [HttpPost]
        public async Task<IActionResult> FormGonderIletisim(EmailSenderViewModel model)
        {
            string result = "";
            try
            {
                string messages = model.content;
                var message = new Message()
                {
                    To = "emre@ikifikir.net",
                    Subject = model.subject,
                    Phone = "",
                    Email = model.email,
                    GuestCount = "",
                    NameSurname = model.namesurname,
                    Content = $@"<p>{model.namesurname} iletişim formunu doldurdu. (Bu form https://ikifikir.net/iletisim üzerinden gelmiştir.) <p> <hr/> <p>Email Adresi: {model.email}</p> <hr/> <p>{messages}</p> <hr/> <p>Telefon: {model.phone}</p>",
                };

                result = await _emailSender.SendEmailAsync(message);

                return RedirectToAction("sonuc", "anasayfa", new { resultPage = result, type = 1 });

            }
            catch (Exception ex)
            {
                return RedirectToAction("sonuc", "anasayfa", new { resultPage = result, type = 0 });
            }

        }

        public IActionResult sonuc(string resultPage, int type)
        {
            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion

            ViewBag.Result = resultPage;
            ViewBag.Type = type;
            return View();
        }

        #endregion

        #region Calculate Price

        [HttpGet]
        public IActionResult fiyatpaketleri()
        {

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion

            var pricingList = _mapper.Map<List<PricingListItemDto>, List<PricingListViewModel>>(_pricingService.getPricingList());

            return View(pricingList);
        }

        [HttpGet]
        public IActionResult fiyathesapla(int Id)
        {
            TempData["isTransparent"] = 0;
            TempData["isHaveFooter"] = 0;

            decimal totalPrice = 0;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = "İkifikir İletişim";
            meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
            meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
            meta.ogTitle = "İkifikir İletişim";
            meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
            meta.Url = "https://www.ikifikir.net";
            ViewBag.Meta = meta;

            #endregion

            var value = _mapper.Map<PricingDto, PricingViewModel>(_pricingService.getPricingById(Id));

            List<PricingComponentListViewModel> components = _mapper.Map<List<PricingComponentListItemDto>, List<PricingComponentListViewModel>>(_pricingService.getPricingComponentListByPricePackageId(value.Id));

            ViewBag.Bilesenler = components;

            List<PricingComponentTypeListViewModel> types = _mapper.Map<List<PricingComponentTypeListItemDto>, List<PricingComponentTypeListViewModel>>(_pricingService.getPricingComponentTypeList());

            ViewBag.Tipler = types;

            var pricingList = _mapper.Map<List<PricingListItemDto>, List<PricingListViewModel>>(_pricingService.getPricingList());
            ViewBag.Paketler = pricingList;

            var cookie = Request.Cookies["ComponentList"];

            if(cookie != null)
            {
                List<ComponentResult> resultList = JsonSerializer.Deserialize<List<ComponentResult>>(cookie);

                if (resultList != null)
                {
                    foreach (var item in resultList)
                    {
                        totalPrice += item.Price;
                    }

                    ViewBag.Total = totalPrice;
                    ViewBag.ComponentChoosed = resultList;
                }
            }
          
            return View(value);
        }

        [HttpPost]
        public JsonResult secimiOnayla(int[] Ids)
        {
            try
            {
                List<ComponentResult> result = new List<ComponentResult>();

                var cookieIsHave = Request.Cookies["ComponentList"];

                foreach (var item in Ids)
                {
                    var component = _pricingService.getPricingComponentById(item);

                    if (component != null)
                    {
                        ComponentResult data = new ComponentResult
                        {
                            Title = component.ComponentTitle,
                            Price = component.Price
                        };

                        result.Add(data);
                    }
                }

                #region Cookie


                if (cookieIsHave != null)
                {
                    List<ComponentResult> cookieResult = JsonSerializer.Deserialize<List<ComponentResult>>(cookieIsHave);
                    result.AddRange(cookieResult);
                    string componentList = JsonSerializer.Serialize(result);
                    Response.Cookies.Append("ComponentList", componentList);
                }
                else
                {
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddYears(1);
                    string componentList = JsonSerializer.Serialize(result);
                    Response.Cookies.Append("ComponentList", componentList, cookie);
                }            

                #endregion

                return Json(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        [HttpGet]
        public IActionResult fiyatOnayla()
        {
            try
            {
                decimal totalPrice = 0;
                var cookie = Request.Cookies["ComponentList"];

                if(cookie != null)
                {
                    TempData["isTransparent"] = 0;

                    #region Meta

                    MetaViewModel meta = new MetaViewModel();
                    meta.Title = "İkifikir İletişim";
                    meta.Keywords = "ikifikir, iletişim, showreels, kurumsal, dijital";
                    meta.Description = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
                    meta.Image = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
                    meta.ogDescription = "ikifikir İletişim | Abra Kadabra! Yıllardan beri İstanbul’da pazarlama iletişimi alanında başarılı işlere imza atan iki ekip, aynı yolda birlikte yürümeye karar verdi.";
                    meta.ogTitle = "İkifikir İletişim";
                    meta.ogImage = "https://uploads.ikifikir.net/site/logo-dark-ikifikir.png";
                    meta.Url = "https://www.ikifikir.net";
                    ViewBag.Meta = meta;

                    #endregion

                    List<ComponentResult> resultList = JsonSerializer.Deserialize<List<ComponentResult>>(cookie);

                    foreach (var item in resultList)
                    {
                        totalPrice += item.Price;
                    }

                    var packagePrice = _pricingService.getPricingById(1);

                    ViewBag.PackageTitle = packagePrice.Title;
                    ViewBag.Total = totalPrice;
                    ViewBag.ResultList = resultList;
                    return View();
                }
                else
                {
                    return RedirectToAction("fiyatpaketleri", "anasayfa");
                }
           

            }
            catch (Exception ex)
            {
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> fiyatformgonder(AppoinmentContact contact)
        {
            string result = "";
            string items = "";
            try
            {
                decimal total = 0;
                List<ComponentResult> resultList = SessionExtensionMethod.GetObject<List<ComponentResult>>(HttpContext.Session, "result");

                foreach (var item in resultList)
                {
                    total += item.Price;
                    items += $"<tr><td style='border: 1px solid black; border-collapse: collapse; padding: 5px;'>{item.Title}</td><td style='border: 1px solid black; border-collapse: collapse; padding: 5px;'>{Convert.ToInt32(item.Price)}₺</td></tr>"; 
                }

                AppoinmentContact contactMessage = new AppoinmentContact();
                contactMessage.EmailAdress = contact.EmailAdress;
                contactMessage.CompanyName = contact.CompanyName;
                contactMessage.Subject = contact.NameSurname + " Fiyat Teklif Başvurusu";
                contactMessage.PhoneNumber = contact.PhoneNumber;
                contactMessage.NameSurname = contact.NameSurname;
                contactMessage.Message = contact.Message;
                contactMessage.To = "emre@ikifikir.net";
                contactMessage.Total = Convert.ToInt32(total);
                contactMessage.componentResult = resultList;
                contactMessage.Content = $@"<p>{contactMessage.NameSurname} iletişim formunu doldurdu. (Bu form https://ikifikir.net/iletisim üzerinden gelmiştir.) <p> <hr/> <p><strong>Email Adresi:</strong> {contactMessage.EmailAdress}</p> <hr/> <p>{contactMessage.Message}</p> <hr/> <p><strong>Telefon: </strong>{contactMessage.PhoneNumber}</p> <hr/> <p><strong>Toplam Teklif:</strong> {contactMessage.Total}₺</p> <hr/> <p><strong>Hizmetler:</strong></p> <table style='table-layout: auto; width: 215px;'><thead><tr><th style='text-align:center; border: 1px solid black; border-collapse: collapse; padding:5px;'>Hizmet Adı</th><th style='text-align:left; border: 1px solid black; padding:5px; border-collapse: collapse;'>Fiyat</th></tr></thead><tbody>{items}</tbody></table>";

                result = await _emailSender.SendEmailAsync(contactMessage);

                //HttpContext.Session.Remove("result");
                //HttpContext.Session.Clear();
                Response.Cookies.Delete("ComponentList");

                return RedirectToAction("sonuc", "anasayfa", new { resultPage = result, type = 1 });
            }
            catch (Exception ex)
            {
                Response.Cookies.Delete("ComponentList");

                //HttpContext.Session.Remove("result");
                //HttpContext.Session.Clear();

                return RedirectToAction("sonuc", "anasayfa", new { resultPage = result, type = 0 });
            }
         
        }

        public IActionResult email_calculate_complete()
        {
            return View();
        }

        public IActionResult hepsinitemizle()
        {
            Response.Cookies.Delete("ComponentList");
            return RedirectToAction("fiyatpaketleri", "anasayfa");
        }

        public IActionResult geridon()
        {
            return RedirectToAction("fiyatpaketleri", "anasayfa");
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ikifikir.COMMON.DataTransfer.CategoryData;
using ikifikir.COMMON.DataTransfer.PostData;
using ikifikir.CORE.Helper.Extends;
using ikifikir.ENGINES.Interface;
using ikifikirweb.Models.MetaConfig;
using ikifikirweb.ViewModels.CategoryModel;
using ikifikirweb.ViewModels.PostModel;
using Microsoft.AspNetCore.Mvc;

namespace ikifikirweb.Controllers
{
    public class blogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        public blogController(IPostService postService, IMapper mapper, ICategoryService categoryService)
        {
            _postService = postService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult icerik(int? pageNumber)
        {
            TempData["isTransparent"] = 0;

            var categories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());
            ViewBag.Categories = categories;

            int pageSize = 10;
            List<PostListViewModel> values;
            values = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.getPostList());

            return View(PaginationList<PostListViewModel>.Create(values.ToList(), pageNumber ?? 1, pageSize));
        }

        [HttpGet("detay/{Id}/{title}", Name = "detay")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult detay(int Id, string title)
        {
            TempData["isTransparent"] = 0;

            var value = _mapper.Map<PostDto, PostViewModel>(_postService.getPostById(Id));
            string friendlyTitle = title;

            var categories = _mapper.Map<List<CategoryListItemDto>, List<CategoryListViewModel>>(_categoryService.getCategoryList());
            ViewBag.Categories = categories;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = value.seoTitle;
            //meta.Keywords = newsGet.Tag;
            meta.Description = value.seoSpot;
            meta.Image = "https://uploads.ikifikir.net/images/" + value.image;
            meta.ogDescription = value.seoSpot;
            meta.ogTitle = value.seoTitle;
            meta.ogImage = "https://uploads.ikifikir.net/images/" + value.image;
            meta.Url = "https://ikifikir.net/" + Id + value.seoTitle;
            ViewBag.Meta = meta;

            #endregion
        
            if (!string.Equals(friendlyTitle, title, StringComparison.Ordinal))
            {
                return this.RedirectToRoutePermanent("detay", new { id = Id, title = friendlyTitle });
            }
            return View(value);
        }
    }
}

using AutoMapper;
using ikifikir.COMMON.DataTransfer.PostData;
using ikifikir.CORE.Helper.Extends;
using ikifikir.editor.Models.PostModel;
using ikifikir.editor.Models.ProjectModel;
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
    public class blogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        public blogController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult icerikler(int? pageNumber, string searchData)
        {
            int pageSize = 50;
            List<PostListViewModel> values;

            if (searchData != "" && searchData != null)
            {
                values = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.searchDataInPost(searchData));
            }
            else
            {
                values = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.getPostList());
            }

            return View(PaginationList<PostListViewModel>.Create(values.ToList(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        [Authorize]
        public IActionResult icerikekle()
        {
            return View(new PostCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> icerikolustur(PostCreateViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.image = SaveFileProcess.ImageInsert(file, "Admin");
                    }
                    else
                    {
                        model.image = "bg.jpg";
                    }

                    if (await _postService.insertPost(_mapper.Map<PostCreateViewModel, PostDto>(model)))
                        return RedirectToAction("icerikler", "blog");
                    else
                        return RedirectToAction("icerikler", "blog");

                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("icerikler", "blog");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult icerikduzenle(int id)
        {
            var value = _mapper.Map<PostDto, PostEditViewModel>(_postService.getPostById(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> icerikguncelle(PostEditViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.image = SaveFileProcess.ImageInsert(file, "Admin");
                    }

                    if (await _postService.updatePost(_mapper.Map<PostEditViewModel, PostDto>(model)))
                        return RedirectToAction("icerikler", "blog");
                    else
                        return RedirectToAction("icerikduzenle", "blog", new { Id = model.Id });

                }
                else
                {
                    return RedirectToAction("icerikduzenle", "blog", new { Id = model.Id });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("icerikduzenle", "blog", new { Id = model.Id });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult iceriksil(int id)
        {
            if (_postService.deletePost(id))
                return RedirectToAction("icerikler", "blog");
            else
                return RedirectToAction("icerikler", "blog");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> icerikaktiflestir(int id)
        {
            if (await _postService.postIsActiveProcess(id))
            {
                return RedirectToAction(nameof(icerikler));
            }
            return RedirectToAction(nameof(icerikler));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> icerikbildirimduzenle(int id)
        {
            if (await _postService.postNotificationEdit(id))
            {
                return RedirectToAction(nameof(icerikler));
            }
            return RedirectToAction(nameof(icerikler));
        }

    }
}

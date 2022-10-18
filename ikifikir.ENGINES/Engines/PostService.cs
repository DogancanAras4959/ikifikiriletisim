using ikifikir.COMMON.DataTransfer.PostData;
using ikifikir.CORE.UnitOfWork;
using ikifikir.DAL;
using ikifikir.DAL.Models;
using ikifikir.ENGINES.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Engines
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public PostService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool deletePost(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<post>().DeleteAsync(new post { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public PostDto getPostById(int id)
        {
            post getPost = _unitOfWork.GetRepository<post>().Where(x => x.Id == id, x => x.OrderBy(y => y.Id), "", null, null).SingleOrDefault();

            if (getPost == null)
            {
                return new PostDto();
            }

            return new PostDto
            {
                content = getPost.content,
                IsActive = getPost.IsActive,
                CreatedTime = getPost.CreatedTime,
                UpdatedTime = getPost.UpdatedTime,
                seoSpot = getPost.seoSpot,
                seoTitle = getPost.seoTitle,
                sorted = getPost.sorted,
                spot = getPost.spot,
                isNotification = getPost.isNotification,
                image = getPost.image,
                title = getPost.title,
                author = getPost.author,
                keywords = getPost.keywords,
                Id = getPost.Id
            };
        }

        public List<PostListItemDto> getPostList()
        {
            IEnumerable<post> roles = _unitOfWork.GetRepository<post>().Where(x => x.Id > 0, x => x.OrderBy(y => y.Id), "", null, null);

            return roles.Select(x => new PostListItemDto
            {
                Id = x.Id,
                content = x.content,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                seoSpot = x.seoSpot,
                seoTitle = x.seoTitle,
                sorted = x.sorted,
                spot = x.spot,
                image = x.image,
                title = x.title,
                isNotification = x.isNotification,
                author = x.author,
                keywords = x.keywords,

            }).ToList();
        }

        public List<PostListItemDto> getPostListByNotification()
        {
            IEnumerable<post> roles = _unitOfWork.GetRepository<post>().Where(x => x.isNotification == true, x => x.OrderBy(y => y.Id), "", null, null);

            return roles.Select(x => new PostListItemDto
            {
                Id = x.Id,
                content = x.content,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                seoSpot = x.seoSpot,
                seoTitle = x.seoTitle,
                sorted = x.sorted,
                spot = x.spot,
                image = x.image,
                title = x.title,
                isNotification = x.isNotification,
                author = x.author,
                keywords = x.keywords,

            }).ToList();
        }

        public List<PostListItemDto> getPostTakeToLast()
        {
            IEnumerable<post> roles = _unitOfWork.GetRepository<post>().Where(x => x.IsActive == true, x => x.OrderByDescending(y => y.Id), "", null, null);

            return roles.Select(x => new PostListItemDto
            {
                Id = x.Id,
                content = x.content,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                seoSpot = x.seoSpot,
                seoTitle = x.seoTitle,
                sorted = x.sorted,
                spot = x.spot,
                image = x.image,
                title = x.title,
                isNotification = x.isNotification,
                author = x.author,
                keywords = x.keywords,

            }).Take(1).ToList();
        }

        public async Task<bool> insertPost(PostDto model)
        {
            post getPost = await _unitOfWork.GetRepository<post>().AddAsync(new post
            {
                content = model.content,
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                seoSpot = model.seoSpot,
                seoTitle = model.seoTitle,
                isNotification = model.isNotification,
                sorted = model.sorted,
                spot = model.spot,
                image = model.image,
                title = model.title,
                author = model.author,
                keywords = model.keywords,
            });;

            return getPost != null && getPost.Id != 0;
        }

        public async Task<bool> postIsActiveProcess(int id)
        {
            post getPost = _unitOfWork.GetRepository<post>().FindAsync(x => x.Id == id).Result;
            if (getPost.IsActive == false)
            {
                getPost.IsActive = true;
                post model = await _unitOfWork.GetRepository<post>().UpdateAsync(getPost);
                return getPost != null;
            }
            else
            {
                getPost.IsActive = false;
                post model = await _unitOfWork.GetRepository<post>().UpdateAsync(getPost);
                return getPost != null;
            }
        }

        public async Task<bool> postNotificationEdit(int id)
        {
            post getPost = _unitOfWork.GetRepository<post>().FindAsync(x => x.Id == id).Result;
            if (getPost.isNotification == false)
            {
                getPost.isNotification = true;
                post model = await _unitOfWork.GetRepository<post>().UpdateAsync(getPost);
                return getPost != null;
            }
            else
            {
                getPost.isNotification = false;
                post model = await _unitOfWork.GetRepository<post>().UpdateAsync(getPost);
                return getPost != null;
            }
        }

        public List<PostListItemDto> searchDataInPost(string searchKey)
        {
            try
            {
                IEnumerable<post> getPost = _unitOfWork.GetRepository<post>().Where(null, x => x.OrderByDescending(y => y.Id), "", null, null);

                if (!String.IsNullOrEmpty(searchKey))
                {
                    getPost = getPost.Where(x => x.title!.Contains(searchKey));
                }

                return getPost.Select(x => new PostListItemDto
                {
                    Id = x.Id,
                    CreatedTime = x.CreatedTime,
                    UpdatedTime = x.UpdatedTime,
                    seoSpot = x.seoSpot,
                    seoTitle = x.seoTitle,
                    isNotification = x.isNotification,
                    sorted = x.sorted,
                    spot = x.spot,
                    content = x.content,
                    image = x.image,
                    IsActive = x.IsActive,
                    title = x.title,
                    keywords = x.keywords,
                    author = x.author,

                }).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> updatePost(PostDto model)
        {
            post postGet = await _unitOfWork.GetRepository<post>().FindAsync(x => x.Id == model.Id);

            post getPost = await _unitOfWork.GetRepository<post>().UpdateAsync(new post
            {
                content = model.content,
                IsActive = postGet.IsActive,
                CreatedTime = postGet.CreatedTime,
                UpdatedTime = DateTime.Now,
                seoSpot = model.seoSpot,
                isNotification = model.isNotification,
                seoTitle = model.seoTitle,
                sorted = model.sorted,
                spot = model.spot,
                image = model.image,
                title = model.title,
                keywords = model.keywords,
                author = model.author,
                Id = model.Id,
            });

            return getPost != null;
        }

    }
}

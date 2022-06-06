using ikifikir.COMMON.DataTransfer.VideoData;
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
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public VideoService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool deleteVideo(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<videos>().DeleteAsync(new videos { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public List<VideoListItemDto> getGalleryVideoListByProjectId(int id)
        {
            IEnumerable<videos> roles = _unitOfWork.GetRepository<videos>().Where(x => x.projectId == id, x => x.OrderBy(y => y.Id), "projectVideos", null, null);

            return roles.Select(x => new VideoListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                iframe = x.iframe,
                slug = x.slug,
                projectId = x.projectId,

            }).ToList();
        }

        public VideoDto getVideoById(int id)
        {
            videos gallery = _unitOfWork.GetRepository<videos>().FindAsync(x => x.Id == id).Result;
            if (gallery == null)
            {
                return new VideoDto();
            }

            return new VideoDto
            {
                slug = gallery.slug,
                CreatedTime = gallery.CreatedTime,
                IsActive = gallery.IsActive,
                projectId = gallery.projectId,
                UpdatedTime = gallery.UpdatedTime,
                iframe = gallery.iframe,
                Id = gallery.Id
            };
        }

        public List<VideoListItemDto> getVideoListMedia()
        {
            IEnumerable<videos> roles = _unitOfWork.GetRepository<videos>().Where(null, x => x.OrderBy(y => y.Id), "projectVideos", null, null);

            return roles.Select(x => new VideoListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                iframe = x.iframe,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                slug = x.slug,
                projectId = x.projectId,

            }).ToList();
        }

        public async Task<bool> insertVideo(VideoDto model)
        {
            try
            {
                videos gal = await _unitOfWork.GetRepository<videos>().AddAsync(new videos
                {
                    IsActive = true,
                    CreatedTime = DateTime.Now,
                    iframe = model.iframe,
                    UpdatedTime = DateTime.Now,
                    slug = model.slug,
                    projectId = model.projectId,
                    name = model.slug,
                });

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}

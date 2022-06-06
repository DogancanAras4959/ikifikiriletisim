using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
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
    public class GalleryService : IGalleryService
    {
        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public GalleryService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool deleteGalleryImage(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<galleries>().DeleteAsync(new galleries { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public GalleryDto getGalleryById(int id)
        {
            galleries gallery = _unitOfWork.GetRepository<galleries>().FindAsync(x => x.Id == id).Result;
            if (gallery == null)
            {
                return new GalleryDto();
            }

            return new GalleryDto
            {
                slug = gallery.slug,
                sorted = gallery.sorted,
                CreatedTime = gallery.CreatedTime,
                IsActive = gallery.IsActive,
                projectId = gallery.projectId,
                UpdatedTime = gallery.UpdatedTime,
                Id = gallery.Id
            };
        }

        public List<GalleryListItemDto> getGalleryImageListByProjectId(int id)
        {
            IEnumerable<galleries> roles = _unitOfWork.GetRepository<galleries>().Where(x => x.projectId == id, x => x.OrderBy(y => y.Id), "projectToGalleries", null, null);

            return roles.Select(x => new GalleryListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                slug = x.slug,
                sorted = x.sorted,
                projectId = x.projectId,

            }).ToList();
        }

        public List<GalleryListItemDto> getGalleryListMedia()
        {
            IEnumerable<galleries> roles = _unitOfWork.GetRepository<galleries>().Where(null, x => x.OrderBy(y => y.Id), "projectToGalleries", null, null);

            return roles.Select(x => new GalleryListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                slug = x.slug,
                sorted = x.sorted,
                projectId = x.projectId,

            }).ToList();
        }

        public async Task<bool> insertGalleryImage(List<GalleryDto> model)
        {
            try
            {
                foreach (var item in model)
                {
                    galleries gal = await _unitOfWork.GetRepository<galleries>().AddAsync(new galleries
                    {
                        IsActive = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        slug = item.slug,
                        projectId = item.projectId,
                        sorted = item.sorted,
                    });
                }

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}

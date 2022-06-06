using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IGalleryService
    {
        #region Admin
        Task<bool> insertGalleryImage(List<GalleryDto> model);
        GalleryDto getGalleryById(int id);
        bool deleteGalleryImage(int id);
        List<GalleryListItemDto> getGalleryImageListByProjectId(int id);
        List<GalleryListItemDto> getGalleryListMedia();

        #endregion

    }
}

using ikifikir.COMMON.DataTransfer.VideoData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IVideoService
    {
        Task<bool> insertVideo(VideoDto model);
        VideoDto getVideoById(int id);
        bool deleteVideo(int id);
        List<VideoListItemDto> getGalleryVideoListByProjectId(int id);
        List<VideoListItemDto> getVideoListMedia();
    }
}

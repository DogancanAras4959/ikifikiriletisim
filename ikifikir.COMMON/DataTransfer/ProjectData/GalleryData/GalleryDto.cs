using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.ProjectData.GalleryData
{
    public class GalleryDto : BaseDto
    {
        public string slug { get; set; }
        public int sorted { get; set; }

        public int projectId { get; set; }
        public project projectToGalleries { get; set; }
    }
}

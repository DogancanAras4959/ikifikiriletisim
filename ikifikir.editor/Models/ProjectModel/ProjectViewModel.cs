using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
using ikifikir.DAL.Models;
using ikifikir.editor.Models.GalleryModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Models.ProjectModel
{
    public class ProjectViewModel : BaseViewModel
    {
        public string seoTitle { get; set; }
        public string seoDescription { get; set; }
        public string projectName { get; set; }
        public string projectSpot { get; set; }
        public string client { get; set; }
        public int parentProjectId { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string imageThumbnail { get; set; }
        public bool isTitle { get; set; }
        public bool isSlider { get; set; }
        public int sorted { get; set; }
        public string tagList { get; set; }
        public int categoryId { get; set; }
        public IFormFileCollection galleries { get; set; }
        public List<GalleryViewModel> galleryList { get; set; }
        public category category { get; set; }
    }
}

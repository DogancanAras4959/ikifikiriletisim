using ikifikir.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.ProjectData
{
    public class ProjectDto : BaseDto
    {
        public string seoTitle { get; set; }
        public string seoDescription { get; set; }
        public string projectName { get; set; }
        public string projectSpot { get; set; }
        public string client { get; set; }
        public string description { get; set; }
        public string tagList { get; set; }
        public bool isTitle { get; set; }
        public bool isSlider { get; set; }
        public int parentProjectId { get; set; }
        public string website { get; set; }
        public string imageThumbnail { get; set; }
        public int sorted { get; set; }
        public int categoryId { get; set; }
        public category category { get; set; }

        //public List<galleries> galleries { get; set; }

    }
}

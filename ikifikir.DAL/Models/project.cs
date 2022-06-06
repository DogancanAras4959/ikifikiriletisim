using ikifikir.DAL.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("project")]
    public class project : GeneralModel, IEntity
    {
        public project()
        {
            galleryList = new List<galleries>();
            videoList = new List<videos>();
            tagProjectsProject = new List<tagProject>();
        }

        public string seoTitle { get; set; }
        public string seoDescription { get; set; }
        public string projectName { get; set; }
        public string projectSpot { get; set; }
        public int parentProjectId { get; set; }
        public string client { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string imageThumbnail { get; set; }
        public int sorted { get; set; }
        public bool isTitle { get; set; }
        public bool isSlider { get; set; }

        [ForeignKey("category")]
        public int categoryId { get; set; }
        public category category { get; set; }
        public List<galleries> galleryList { get; set; }
        public List<videos> videoList { get; set; }
        public virtual ICollection<tagProject> tagProjectsProject { get; set; }

    }
}

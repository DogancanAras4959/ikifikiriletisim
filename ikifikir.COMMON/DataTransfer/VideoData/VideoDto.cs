using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.VideoData
{
    public class VideoDto : BaseDto
    {
        public string slug { get; set; }
        public string name { get; set; }
        public string iframe { get; set; }
        public int projectId { get; set; }
        public project projectVideos { get; set; }
    }
}

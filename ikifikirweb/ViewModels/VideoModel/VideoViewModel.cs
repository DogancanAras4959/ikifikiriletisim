using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.ViewModels.VideoModel
{
    public class VideoViewModel : BaseViewModel
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string iframe { get; set; }
        public int projectId { get; set; }
    }
}

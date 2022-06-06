using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Models.GalleryModel
{
    public class GalleryViewModel : BaseViewModel
    {
        public string slug { get; set; }
        public int sorted { get; set; }
        public int projectId { get; set; }
    }
}

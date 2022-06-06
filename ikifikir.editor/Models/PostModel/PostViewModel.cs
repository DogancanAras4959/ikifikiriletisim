using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Models.PostModel
{
    public class PostViewModel : BaseViewModel
    {
        public string title { get; set; }
        public string spot { get; set; }
        public string content { get; set; }
        public int sorted { get; set; }
        public bool isNotification { get; set; }
        public string seoTitle { get; set; }
        public string seoSpot { get; set; }
        public string keywords { get; set; }
        public string image { get; set; }
        public string author { get; set; }
    }
}

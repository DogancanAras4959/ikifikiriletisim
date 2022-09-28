using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.ViewModels.CategoryModel
{
    public class CategoryViewModel : BaseViewModel
    {
        public string name { get; set; }
        public string filterType { get; set; }
        public string categoryTags { get; set; }
        public List<string> tagsListToCategory { get; set; }

    }
}

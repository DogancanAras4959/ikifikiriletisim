using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.ViewModels.TagProjectModel
{
    public class TagProjectViewModel : BaseViewModel
    {
        public int projectId { get; set; }
        public int tagId { get; set; }

        public project projectToTag { get; set; }
        public tags tagToTag { get; set; }
    }
}

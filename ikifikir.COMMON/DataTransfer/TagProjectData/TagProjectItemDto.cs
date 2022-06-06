using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.TagProjectData
{
    public class TagProjectItemDto : BaseDto
    {
        public int projectId { get; set; }
        public int tagId { get; set; }

        public project projectToTag { get; set; }
        public tags tagToTag { get; set; }
    }
}

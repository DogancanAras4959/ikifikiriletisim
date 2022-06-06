using AutoMapper;
using ikifikir.COMMON.DataTransfer.TagData;
using ikifikir.COMMON.DataTransfer.TagProjectData;
using ikifikir.editor.Models.TagModel;
using ikifikir.editor.Models.TagProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagDto, TagViewModel>();
            CreateMap<TagListItemDto, TagListViewModel>();
            CreateMap<TagCreateViewModel, TagDto>();
            CreateMap<TagDto, TagEditViewModel>();
            CreateMap<TagEditViewModel, TagDto>();

            CreateMap<TagProjectItemDto, TagProjectViewModel>();
            CreateMap<TagProjectListItemDto, TagProjectListViewModel>();
            CreateMap<TagProjectCreateViewModel, TagProjectItemDto>();
            CreateMap<TagProjectItemDto, TagProjectEditViewModel>();
            CreateMap<TagProjectEditViewModel, TagProjectItemDto>();
        }
    }
}

using AutoMapper;
using ikifikir.COMMON.DataTransfer.PostData;
using ikifikir.COMMON.DataTransfer.ReferenceData;
using ikifikir.editor.Models.PostModel;
using ikifikir.editor.Models.ReferenceLogoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDto, PostViewModel>();
            CreateMap<PostListItemDto, PostListViewModel>();
            CreateMap<PostCreateViewModel, PostDto>();
            CreateMap<PostDto, PostEditViewModel>();
            CreateMap<PostEditViewModel, PostDto>();

            CreateMap<ReferenceDto, ReferenceLogoListViewModel>();
            CreateMap<ReferenceListItemDto, ReferenceLogoListViewModel>();
            CreateMap<ReferenceLogoCreateViewModel, ReferenceDto>();
        }

    }
}

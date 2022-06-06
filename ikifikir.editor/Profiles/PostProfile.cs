using AutoMapper;
using ikifikir.COMMON.DataTransfer.PostData;
using ikifikir.editor.Models.PostModel;
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
        }

    }
}

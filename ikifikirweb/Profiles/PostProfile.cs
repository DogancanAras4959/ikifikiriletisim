using AutoMapper;
using ikifikir.COMMON.DataTransfer.PostData;
using ikifikirweb.ViewModels.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostListItemDto, PostListViewModel>();
            CreateMap<PostDto, PostViewModel>();
        }
    }
}

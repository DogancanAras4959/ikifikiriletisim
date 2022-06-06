using AutoMapper;
using ikifikir.COMMON.DataTransfer.CategoryData;
using ikifikirweb.ViewModels.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.Profiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CategoryListItemDto, CategoryListViewModel>();
        }
    }
}

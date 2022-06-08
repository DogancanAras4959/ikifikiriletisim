using AutoMapper;
using ikifikir.COMMON.DataTransfer.CategoryData;
using ikifikir.COMMON.DataTransfer.ProjectData;
using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
using ikifikir.COMMON.DataTransfer.ReferenceData;
using ikifikir.COMMON.DataTransfer.VideoData;
using ikifikir.editor.Models.CategoryModel;
using ikifikir.editor.Models.GalleryModel;
using ikifikir.editor.Models.ProjectModel;
using ikifikir.editor.Models.ReferenceLogoModel;
using ikifikir.editor.Models.VideoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, ProjectViewModel>();
            CreateMap<ProjectListItemDto, ProjectListViewModel>();
            CreateMap<ProjectCreateViewModel, ProjectDto>();
            CreateMap<ProjectDto, ProjectEditViewModel>();
            CreateMap<ProjectEditViewModel, ProjectDto>();

            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CategoryListItemDto, CategoryListViewModel>();
            CreateMap<CategoryCreateViewModel, CategoryDto>();
            CreateMap<CategoryDto, CategoryEditViewModel>();
            CreateMap<CategoryEditViewModel, CategoryDto>();

            CreateMap<GalleryViewModel, GalleryDto>();
            CreateMap<GalleryDto, GalleryViewModel>();
            CreateMap<GalleryListItemDto, GalleryListViewModel>();
            CreateMap<GalleryCreateViewModel, GalleryDto>();
            CreateMap<GalleryDto, GalleryEditViewModel>();
            CreateMap<GalleryEditViewModel, GalleryDto>();

            CreateMap<VideoViewModel, VideoDto>();
            CreateMap<VideoDto, VideoViewModel>();
            CreateMap<VideoListItemDto, VideoListViewModel>();
            CreateMap<VideoCreateViewModel, VideoDto>();
            CreateMap<VideoDto, VideoEditViewModel>();
            CreateMap<VideoEditViewModel, GalleryDto>();

            CreateMap<ReferenceLogoListViewModel, ReferenceListItemDto>();

        }
    }
}

using AutoMapper;
using ikifikir.COMMON.DataTransfer.ProjectData;
using ikifikir.COMMON.DataTransfer.ProjectData.GalleryData;
using ikifikir.COMMON.DataTransfer.TagProjectData;
using ikifikir.COMMON.DataTransfer.VideoData;
using ikifikirweb.ViewModels.GalleryModel;
using ikifikirweb.ViewModels.ProjectModel;
using ikifikirweb.ViewModels.TagProjectModel;
using ikifikirweb.ViewModels.VideoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.Profiles
{
    public class ProjectProfiles : Profile
    {
        public ProjectProfiles()
        {
            CreateMap<ProjectListItemDto, ProjectListViewModel>();
            CreateMap<ProjectDto, ProjectViewModel>();
            CreateMap<VideoListItemDto, VideoListViewModel>();
            CreateMap<GalleryListItemDto, GalleryListViewModel>();
            CreateMap<TagProjectListItemDto,TagProjectListViewModel>();
        }
    }
}

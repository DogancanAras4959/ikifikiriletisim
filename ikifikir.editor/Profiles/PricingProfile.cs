using AutoMapper;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentTypeData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingData;
using ikifikir.editor.Models.PricingDataModel.PricingComponentModel;
using ikifikir.editor.Models.PricingDataModel.PricingComponentTypeModel;
using ikifikir.editor.Models.PricingDataModel.PricingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Profiles
{
    public class PricingProfile : Profile
    {
        public PricingProfile()
        {
            CreateMap<PricingDto, PricingViewModel>();
            CreateMap<PricingListItemDto, PricingListViewModel>();
            CreateMap<PricingCreateViewModel, PricingDto>();
            CreateMap<PricingDto, PricingEditViewModel>();
            CreateMap<PricingEditViewModel, PricingDto>();

            CreateMap<PricingComponentDto, PricingComponentViewModel>();
            CreateMap<PricingComponentListItemDto, PricingComponentListViewModel>();
            CreateMap<PricingComponentCreateViewModel, PricingComponentDto>();
            CreateMap<PricingComponentDto, PricingComponentViewModel>();
            CreateMap<PricingComponentEditViewModel, PricingComponentDto>();

            CreateMap<PricingComponentTypeDto, PricingComponentTypeViewModel>();
            CreateMap<PricingComponentTypeListItemDto, PricingComponentTypeListViewModel>();
            CreateMap<PricingComponentTypeCreateViewModel, PricingComponentTypeDto>();
            CreateMap<PricingComponentTypeDto, PricingComponentTypeViewModel>();
            CreateMap<PricingComponentTypeEditViewModel, PricingComponentTypeDto>();
        }
    }
}

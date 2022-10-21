using AutoMapper;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentTypeData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingData;
using ikifikir.DAL.Models;
using ikifikirweb.ViewModels.PricingDataModelWeb.PricingComponentTypeWebModel;
using ikifikirweb.ViewModels.PricingDataModelWeb.PricingComponentWebModel;
using ikifikirweb.ViewModels.PricingDataModelWeb.PricingWebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.Profiles
{
    public class PricingProfile : Profile
    {
        public PricingProfile()
        {

            CreateMap<PricingDto, PricingViewModel>();
            CreateMap<PricingListItemDto, PricingListViewModel>();

            CreateMap<PricingComponentDto, PricingComponentViewModel>();
            CreateMap<PricingComponentListItemDto, PricingComponentListViewModel>();

            CreateMap<PricingComponentTypeDto, PricingComponentTypeViewModel>();
            CreateMap<PricingComponentTypeListItemDto, PricingComponentTypeListViewModel>();
        }
    }
}

using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentTypeData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IPricingService
    {

        #region Pricing

        Task<bool> insertPricing(PricingDto model);
        Task<bool> updatePricing(PricingDto model);
        bool deletePricing(int id);
        PricingDto getPricingById(int id);
        List<PricingListItemDto> getPricingList();
        List<PricingComponentListItemDto> getPricingComponentListByPricePackageId(int Id);

        #endregion

        #region PricingComponents

        Task<bool> insertPricingComponent(PricingComponentDto model);
        Task<bool> updatePricingComponent(PricingComponentDto model);
        bool deletePricingComponent(int id);
        PricingComponentDto getPricingToPriceValue(decimal priceValue);
        PricingComponentDto getPricingComponentById(int id);
        List<PricingComponentListItemDto> getPricingComponentList();
        List<PricingComponentTypeListItemDto> getPricingComponentTypeByTypeId(int Id);


        #endregion

        #region PricingComponentsType

        Task<bool> insertPricingComponentTypes(PricingComponentTypeDto model);
        Task<bool> updatePricingComponentType(PricingComponentTypeDto model);
        bool deletePricingComponentType(int id);
        PricingComponentTypeDto getPricingComponentTypeById(int id);
        List<PricingComponentTypeListItemDto> getPricingComponentTypeList();

        #endregion

    }
}

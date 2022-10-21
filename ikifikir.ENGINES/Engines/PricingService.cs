using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentTypeData;
using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingData;
using ikifikir.CORE.UnitOfWork;
using ikifikir.DAL;
using ikifikir.DAL.Models;
using ikifikir.ENGINES.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Engines
{
    public class PricingService : IPricingService
    {

        private readonly IUnitOfWork<ikifikirdbcontext> _unitOfWork;
        public PricingService(IUnitOfWork<ikifikirdbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool deletePricing(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<pricing>().DeleteAsync(new pricing { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public bool deletePricingComponent(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<pricingComponents>().DeleteAsync(new pricingComponents { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public bool deletePricingComponentType(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<pricingComponentTypes>().DeleteAsync(new pricingComponentTypes { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public PricingDto getPricingById(int id)
        {
            pricing getPost = _unitOfWork.GetRepository<pricing>().Where(x => x.Id == id, x => x.OrderBy(y => y.Id), "", null, null).SingleOrDefault();

            if (getPost == null)
            {
                return new PricingDto();
            }

            return new PricingDto
            {
                IsActive = getPost.IsActive,
                CreatedTime = getPost.CreatedTime,
                UpdatedTime = getPost.UpdatedTime,
                MonthPrice = getPost.MonthPrice,
                Status = getPost.Status,
                Image = getPost.Image,
                Title = getPost.Title,
                YearPrice = getPost.YearPrice,
                Id = getPost.Id
            };
        }

        public PricingComponentDto getPricingComponentById(int id)
        {
            pricingComponents getPost = _unitOfWork.GetRepository<pricingComponents>().Where(x => x.Id == id, x => x.OrderBy(y => y.Id), "", null, null).SingleOrDefault();

            if (getPost == null)
            {
                return new PricingComponentDto();
            }

            return new PricingComponentDto
            {
                IsActive = getPost.IsActive,
                CreatedTime = getPost.CreatedTime,
                UpdatedTime = getPost.UpdatedTime,
                ComponentTitle = getPost.ComponentTitle,
                Price = getPost.Price,
                PricingId = getPost.PricingId,
                Id = getPost.Id,
                ChooseType = getPost.ChooseType,
                
            };
        }

        public List<PricingComponentListItemDto> getPricingComponentList()
        {
            IEnumerable<pricingComponents> roles = _unitOfWork.GetRepository<pricingComponents>().Where(x => x.Id > 0, x => x.OrderBy(y => y.Id), "pricing", null, null);

            return roles.Select(x => new PricingComponentListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                Price = x.Price,
                PricingId = x.PricingId,
                ComponentTitle = x.ComponentTitle,

            }).ToList();
        }

        public List<PricingComponentListItemDto> getPricingComponentListByPricePackageId(int Id)
        {
            IEnumerable<pricingComponents> components = _unitOfWork.GetRepository<pricingComponents>().Where(x => x.PricingId == Id, x => x.OrderBy(y => y.Id), "pricing", null, null);

            return components.Select(x => new PricingComponentListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                Price = x.Price,
                PricingId = x.PricingId,
                pricing = x.pricing,
                ComponentTitle = x.ComponentTitle,

            }).ToList();
        }

        public PricingComponentTypeDto getPricingComponentTypeById(int id)
        {
            pricingComponentTypes getPost = _unitOfWork.GetRepository<pricingComponentTypes>().Where(x => x.Id == id, x => x.OrderBy(y => y.Id), "", null, null).SingleOrDefault();

            if (getPost == null)
            {
                return new PricingComponentTypeDto();
            }

            return new PricingComponentTypeDto
            {
                IsActive = getPost.IsActive,
                CreatedTime = getPost.CreatedTime,
                UpdatedTime = getPost.UpdatedTime,
                Price = getPost.Price,
                Type = getPost.Type,
                Id = getPost.Id
            };
        }

        public List<PricingComponentTypeListItemDto> getPricingComponentTypeByTypeId(int Id)
        {
            IEnumerable<pricingComponentTypes> roles = _unitOfWork.GetRepository<pricingComponentTypes>().Where(x => x.pricingComponentId == Id, x => x.OrderBy(y => y.Id), "pricingComponents", null, null);

            return roles.Select(x => new PricingComponentTypeListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                pricingComponentId = x.pricingComponentId,
                Price = x.Price,
                pricingComponents = x.pricingComponents,
                Type = x.Type,

            }).ToList();
        }

        public List<PricingComponentTypeListItemDto> getPricingComponentTypeList()
        {
            IEnumerable<pricingComponentTypes> roles = _unitOfWork.GetRepository<pricingComponentTypes>().Where(x => x.Id > 0, x => x.OrderBy(y => y.Id), "pricingComponents", null, null);

            return roles.Select(x => new PricingComponentTypeListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                Price = x.Price,
                pricingComponentId = x.pricingComponentId,
                Type = x.Type,

            }).ToList();
        }

        public List<PricingListItemDto> getPricingList()
        {
            IEnumerable<pricing> roles = _unitOfWork.GetRepository<pricing>().Where(x => x.Id > 0, x => x.OrderBy(y => y.Id), "", null, null);

            return roles.Select(x => new PricingListItemDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                Image = x.Image,
                UpdatedTime = x.UpdatedTime,
                Status = x.Status,
                MonthPrice = x.MonthPrice,
                Title = x.Title,
                YearPrice = x.YearPrice

            }).ToList();
        }

        public PricingComponentDto getPricingToPriceValue(decimal priceValue)
        {
            pricingComponents getPost = _unitOfWork.GetRepository<pricingComponents>().Where(x => x.Price == priceValue, x => x.OrderBy(y => y.Id), "", null, null).SingleOrDefault();

            if (getPost == null)
            {
                return new PricingComponentDto();
            }

            return new PricingComponentDto
            {
                IsActive = getPost.IsActive,
                CreatedTime = getPost.CreatedTime,
                UpdatedTime = getPost.UpdatedTime,
                ComponentTitle = getPost.ComponentTitle,
                Price = getPost.Price,
                PricingId = getPost.PricingId,
                Id = getPost.Id,
                ChooseType = getPost.ChooseType,

            };
        }

        public async Task<bool> insertPricing(PricingDto model)
        {
            pricing getPost = await _unitOfWork.GetRepository<pricing>().AddAsync(new pricing
            {
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Image = model.Image,
                Status = model.Status,
                MonthPrice = model.MonthPrice,
                Title = model.Title,
                YearPrice = model.YearPrice
            }); 

            return getPost != null && getPost.Id != 0;
        }

        public async Task<bool> insertPricingComponent(PricingComponentDto model)
        {
            pricingComponents getPost = await _unitOfWork.GetRepository<pricingComponents>().AddAsync(new pricingComponents
            {
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                ComponentTitle = model.ComponentTitle,
                Price = model.Price,
                PricingId = model.PricingId,
                ChooseType = model.ChooseType,
            });

            return getPost != null && getPost.Id != 0;

        }

        public async Task<bool> insertPricingComponentTypes(PricingComponentTypeDto model)
        {
            pricingComponentTypes getPost = await _unitOfWork.GetRepository<pricingComponentTypes>().AddAsync(new pricingComponentTypes
            {
                IsActive = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Price = model.Price,
                pricingComponentId = model.pricingComponentId,
                Type = model.Type,
            });

            return getPost != null && getPost.Id != 0;

        }

        public async Task<bool> updatePricing(PricingDto model)
        {
            pricing postGet = await _unitOfWork.GetRepository<pricing>().FindAsync(x => x.Id == model.Id);

            pricing getPost = await _unitOfWork.GetRepository<pricing>().UpdateAsync(new pricing
            {
                IsActive = postGet.IsActive,
                CreatedTime = postGet.CreatedTime,
                UpdatedTime = DateTime.Now,
                Image = model.Image,
                YearPrice = model.YearPrice,
                Status = model.Status,
                MonthPrice = model.MonthPrice,
                Title = model.Title,
                Id = model.Id,
            });

            return getPost != null;
        }

        public async Task<bool> updatePricingComponent(PricingComponentDto model)
        {
            pricingComponents postGet = await _unitOfWork.GetRepository<pricingComponents>().FindAsync(x => x.Id == model.Id);

            pricingComponents getPost = await _unitOfWork.GetRepository<pricingComponents>().UpdateAsync(new pricingComponents
            {
                IsActive = postGet.IsActive,
                CreatedTime = postGet.CreatedTime,
                UpdatedTime = DateTime.Now,
                Price = model.Price,
                ComponentTitle = model.ComponentTitle,
                PricingId = model.PricingId,      
                Id = model.Id,
                ChooseType = model.ChooseType,

            });

            return getPost != null;
        }

        public async Task<bool> updatePricingComponentType(PricingComponentTypeDto model)
        {
            pricingComponentTypes postGet = await _unitOfWork.GetRepository<pricingComponentTypes>().FindAsync(x => x.Id == model.Id);

            pricingComponentTypes getPost = await _unitOfWork.GetRepository<pricingComponentTypes>().UpdateAsync(new pricingComponentTypes
            {
                IsActive = postGet.IsActive,
                CreatedTime = postGet.CreatedTime,
                UpdatedTime = DateTime.Now,
                Price = postGet.Price,
                Type = postGet.Type,
                Id = model.Id,
            });

            return getPost != null;
        }

    }
}

using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using KGJ.Authorization.Users;
using KGJ.WareHouse.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using KGJ.BaseDto;

namespace KGJ.WareHouse
{
    public class WareHouseInfoAppService : KGJAppServiceBase
    {

        private readonly IRepository<WareHouseInfo, long> _wareHouseInfoRepository;
        private readonly IRepository<WareHouseInfoDts, long> _wareHouseInfoDtsRepository;
        private readonly IRepository<User, long> _userRepository;
        public WareHouseInfoAppService(IRepository<WareHouseInfo, long> WareHouseInfo, IRepository<WareHouseInfoDts, long> WareHouseInfoDts, IRepository<User, long> User)
        {
            _wareHouseInfoRepository = WareHouseInfo;
            _wareHouseInfoDtsRepository = WareHouseInfoDts;
            _userRepository = User;
        }
        /// <summary>
        /// 获取仓库信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<WareHouseInfoDto>> GetWareHouseInfoAsync(WareHouseInfoInput input)
        {
            var query = from a in _wareHouseInfoRepository.GetAll()
                        join user in _userRepository.GetAll() on a.CreatorUserId.Value equals user.Id
                        //join codes in _systemCodesManager.SystemCodes_WHtype on a.WHType.ToString() equals codes.Key
                        where (string.IsNullOrEmpty(input.Filter) ? 1 == 1 : a.WareHouseName.Contains(input.Filter) || a.WareHouseNo.Contains(input.Filter))
                        select new WareHouseInfoDto
                        {

                            Id = a.Id,
                            WareHouseNo = a.WareHouseNo,
                            WareHouseName = a.WareHouseName,
                            Desc = a.Desc,
                            WHType = a.WHType,
                            //WHTypeName = codes.Value,
                            //SiteNo = a.SiteNo,
                            CreationTime = a.CreationTime,
                            CreatorUserId = a.CreatorUserId,
                            CreatorUserName = user.UserName
                        };
            var count = await query.CountAsync();
            var list = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<WareHouseInfoDto>(count, list);
        }

        /// <summary>
        /// 获取仓库详细信息
        /// </summary>
        /// <param name="wareHouseNo"></param>
        /// <returns></returns>
        public async Task<List<WareHouseInfoDtsDto>> GetWareHouseInfoDts(string wareHouseNo)
        {
            var query = from a in _wareHouseInfoDtsRepository.GetAll()
                        join user in _userRepository.GetAll() on a.CreatorUserId equals user.Id
                        where a.WareHouseNo == wareHouseNo 
                        orderby a.Id
                        select new WareHouseInfoDtsDto
                        {
                            Id = a.Id,
                            WareHouseNo = a.WareHouseNo,
                            LocationNo = a.LocationNo,
                            Capacity = a.Capacity,
                            UsedCapacity = a.UsedCapacity,
                            CreationTime = a.CreationTime,
                            CreatorUserId = a.CreatorUserId,
                            CreatorUserName = user.UserName
                        };
            return await query.ToListAsync();
        }
        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        public async Task<BaseResultDto> CreateOrUpdate(WareHouseInfoCreateDto createDto)
        {
            var result = new BaseResultDto();
            if (createDto.WareHouseInfo.Id.HasValue && createDto.WareHouseInfo.Id.Value > 0)
            {
                var wareHouseInfo = await _wareHouseInfoRepository.GetAsync(createDto.WareHouseInfo.Id.Value);
                wareHouseInfo.WareHouseNo = createDto.WareHouseInfo.WareHouseNo;
                wareHouseInfo.WareHouseName = createDto.WareHouseInfo.WareHouseName;
                wareHouseInfo.Desc = createDto.WareHouseInfo.Desc;
                wareHouseInfo.WHType = createDto.WareHouseInfo.WHType;
                //wareHouseInfo.SiteNo = createDto.WareHouseInfo.SiteNo;
                wareHouseInfo.LastModifierUserId = createDto.WareHouseInfo.LastModifierUserId;
                await _wareHouseInfoRepository.UpdateAsync(wareHouseInfo);
                if (createDto.WareHouseInfoDts == null)
                {
                    result.IsSuccess = true;
                    return result;
                }
                var WHDts = await _wareHouseInfoDtsRepository.GetAllListAsync(c => c.WareHouseNo == createDto.WareHouseInfo.WareHouseNo);

                createDto.WareHouseInfoDts.ForEach(async a =>
                {
                    if (a.Id.HasValue && a.Id.Value > 0)
                    {
                        var WHDetail = WHDts.Find(x => x.Id == a.Id.Value);
                        WHDetail.LocationNo = a.LocationNo;
                        WHDetail.Capacity = a.Capacity;
                        WHDetail.UsedCapacity = a.UsedCapacity;
                        WHDetail.WareHouseNo = createDto.WareHouseInfo.WareHouseNo;
                        WHDetail.LastModifierUserId = createDto.WareHouseInfo.LastModifierUserId;
                        await _wareHouseInfoDtsRepository.UpdateAsync(WHDetail);
                    }
                    else
                    {
                        var WHDetail = ObjectMapper.Map<WareHouseInfoDts>(a);
                        WHDetail.WareHouseNo = createDto.WareHouseInfo.WareHouseNo;
                        WHDetail.CreatorUserId = createDto.WareHouseInfo.CreatorUserId;
                        WHDetail.TenantId = AbpSession.TenantId;
                        await _wareHouseInfoDtsRepository.InsertAsync(WHDetail);
                    }
                });
            }
            else
            {
                var count = _wareHouseInfoRepository.GetAll().Where(c => c.WareHouseNo == createDto.WareHouseInfo.WareHouseNo).Count();
                if (count > 0)
                {
                    result.IsSuccess = false;
                    result.ErrorCode = 250;
                    result.ErrorMessage = "WareHouseNo已经存在";
                    return result;
                }

                var model = ObjectMapper.Map<WareHouseInfo>(createDto.WareHouseInfo);
                model.CreatorUserId = createDto.WareHouseInfo.CreatorUserId;
                model.TenantId = AbpSession.TenantId;
                await _wareHouseInfoRepository.InsertAsync(model);

                if (createDto.WareHouseInfoDts != null)
                {
                    createDto.WareHouseInfoDts.ForEach(async c =>
                    {
                        var WHDtsModel = ObjectMapper.Map<WareHouseInfoDts>(c);
                        WHDtsModel.WareHouseNo = createDto.WareHouseInfo.WareHouseNo;
                        WHDtsModel.CreatorUserId = createDto.WareHouseInfo.CreatorUserId;
                        WHDtsModel.TenantId = AbpSession.TenantId;
                        await _wareHouseInfoDtsRepository.InsertAsync(WHDtsModel);
                    });
                }
            }
            result.IsSuccess = true;
            return result;
        }

        /// <summary>
        /// 列表页面删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BaseResultDto>  UpdateWareHouseInfoAsync(UpdateWareHouseInfoInput input)
        {
            var result = new BaseResultDto();        
            var entity = _wareHouseInfoDtsRepository.GetAll().FirstOrDefault(c => c.WareHouseNo == input.wareHouseNo);
            if (entity==null||entity.UsedCapacity<=0)
            {
                await _wareHouseInfoRepository.DeleteAsync(input.Id);
                result.IsSuccess = true;
            }                         
            else
            {
                result.IsSuccess = false;
                //result.ErrorMessage = "存在物料，不可以删除";
            }
            return result;
        }
        /// <summary>
        /// 详情页面删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateWareHouseInfoDtsAsync(UpdateWareHouseInfoInput input)
        {
            var result = new BaseResultDto();
            var entity = _wareHouseInfoDtsRepository.GetAll().FirstOrDefault(c => c.Id == input.Id);
            if (entity == null || entity.UsedCapacity <= 0)
            {
                await _wareHouseInfoDtsRepository.DeleteAsync(input.Id);
                result.IsSuccess = true;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "存在物料，不可以删除";
            }

        }

        /// <summary>
        /// 下拉框选择库区
        /// </summary>
        /// <param name="wareHouseNo"></param>
        /// <returns></returns>

        public async Task<List<ComboboxItemDto>> GetWareHouseInfoDtsByNo(string wareHouseNo)
        {
            var result = new List<ComboboxItemDto>();
            var query = from a in _wareHouseInfoRepository.GetAll()
                        join b in _wareHouseInfoDtsRepository.GetAll() on a.WareHouseNo equals b.WareHouseNo
                        where a.WareHouseNo == wareHouseNo && a.IsDeleted == false && b.IsDeleted == false
                        select new ComboboxItemDto
                        {
                            DisplayText = b.LocationNo,
                            Value = b.LocationNo
                        };
            result = await query.ToListAsync();
            return result;

        }
        /// <summary>
        /// 下拉框选择仓库
        /// </summary>
        /// <returns></returns>

        public async Task<List<ComboboxItemDto>> GetWareHouseInfoName()
        {
            var result = new List<ComboboxItemDto>();
            var query = from a in _wareHouseInfoRepository.GetAll()
                        where a.IsDeleted == false
                        select new ComboboxItemDto
                        {
                            DisplayText = a.WareHouseName,
                            Value = a.WareHouseNo
                        };
            result = await query.ToListAsync();
            return result;
        }
    }
}

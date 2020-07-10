using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using KGJ.Authorization.Users;
using KGJ.BaseDto;
using KGJ.WareHouse.Dto;
using Microsoft.EntityFrameworkCore;
using static KGJ.KGJEnum;

namespace KGJ.WareHouse
{
    public class WareHouseIOFormAppService : KGJAppServiceBase
    {
        private readonly IRepository<WareHouseInfo, long> _wareHouseInfoRepository;
        private readonly IRepository<WareHouseIOForm, long> _wareHouseIOFormRepository;
        private readonly IRepository<WareHouseIOFormDts, long> _wareHouseIOFormDtsRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public WareHouseIOFormAppService(IRepository<WareHouseInfo, long> wareHouseInfo,
            IRepository<WareHouseIOForm, long> wareHouseIOForm,
            IRepository<WareHouseIOFormDts, long> wareHouseIOFormDts,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _wareHouseInfoRepository = wareHouseInfo;
            _wareHouseIOFormRepository = wareHouseIOForm;
            _wareHouseIOFormDtsRepository = wareHouseIOFormDts;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 获取进料单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<WareHouseIOFormDto>> GetWareHouseIOFormAsync(WareHouseIOFormInput input)
        {
            var query = (from a in _wareHouseIOFormRepository.GetAll()
                         join user in _userManager.Users on a.CreatorUserId equals user.Id
                         //供应商//数据字典类型
                         join WH in _wareHouseInfoRepository.GetAll() on a.WareHouseNo equals WH.WareHouseNo into WHJ
                         from WH in WHJ.DefaultIfEmpty()
                         select new WareHouseIOFormDto
                         {
                             Id = a.Id,
                             TicketNo = a.TicketNo,
                             WareHouseNo = a.WareHouseNo,
                             WareHouseName = WH.WareHouseName,
                             //SupplierNo=,
                             //SupplierName=,
                             Status = a.Status,
                             //StatusName=,
                             IOType = a.IOType,
                             Remark = a.Remark,
                             CreationTime = a.CreationTime,
                             CreatorUserId = a.CreatorUserId,
                             CreatorUserName = user.UserName
                         })
                .WhereIf(!string.IsNullOrEmpty(input.Filter), c => c.TicketNo.Contains(input.Filter) || c.IOType == input.IOType);
            var count = await query.CountAsync();
            var list = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<WareHouseIOFormDto>(count, list);
        }

        /// <summary>
        /// 新增/编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<TicketResultDto> CreateOrUpdate(WareHouseIOFormCreateDto dto)
        {
            var result = new TicketResultDto();
            result.BaseResultDto = new BaseResultDto();
            var iOType= dto.WareHouseIOForm.IOType;
            //编辑
            if (dto.WareHouseIOForm.Id.HasValue && dto.WareHouseIOForm.Id.Value > 0)
            {
               var wareHouseIOForm =  ObjectMapper.Map<WareHouseIOForm>(dto.WareHouseIOForm);
                wareHouseIOForm.Status = KGJEnum.WareHouseIOFormStatusEnum.New.GetHashCode();
                await _wareHouseIOFormRepository.UpdateAsync(wareHouseIOForm);

                if (dto.WareHouseIOFormDts==null)
                {
                    result.BaseResultDto.IsSuccess = true;
                    return result;
                }
               var WHIODtslist= await  _wareHouseIOFormDtsRepository.GetAllListAsync(c => c.TicketNo == dto.WareHouseIOForm.TicketNo);
                foreach (var item in dto.WareHouseIOFormDts)
                {
                    if (item.Id.HasValue && item.Id.Value > 0)
                    {
                        var WHIODetail = WHIODtslist.Find(x => x.Id == item.Id.Value);
                        if (WHIODetail == null)
                        {
                            result.BaseResultDto.ErrorMessage = "进料单详细数据没有在数据库中找到！";
                            result.BaseResultDto.IsSuccess = false;
                            result.BaseResultDto.ErrorCode = 400;
                            return result;
                        }
                        WHIODetail= ObjectMapper.Map<WareHouseIOFormDts>(item);
                        WHIODetail.TicketNo = dto.WareHouseIOForm.TicketNo;
                        WHIODetail.LastModifierUserId = dto.WareHouseIOForm.LastModifierUserId;
                        await _wareHouseIOFormDtsRepository.UpdateAsync(WHIODetail);
                    }
                    else
                    {
                        var  WHIODetail = ObjectMapper.Map<WareHouseIOFormDts>(item);
                        WHIODetail.TicketNo = dto.WareHouseIOForm.TicketNo;
                        WHIODetail.LastModifierUserId = dto.WareHouseIOForm.LastModifierUserId;
                        await _wareHouseIOFormDtsRepository.InsertAsync(WHIODetail);
                    }                  
                }
            }
            //新增
            else 
            {
                var count = _wareHouseIOFormRepository.GetAll().Where(c => c.TicketNo == dto.WareHouseIOForm.TicketNo).Count();
                if (count >0 )
                {
                    result.BaseResultDto.IsSuccess = false;
                    result.BaseResultDto.ErrorCode = 250;
                    result.BaseResultDto.ErrorMessage = "TicketNo已经存在！";
                    return result;
                }
                var model=  ObjectMapper.Map<WareHouseIOForm>(dto.WareHouseIOForm);
                //生成单号规则
                model.TicketNo = "";

                await _wareHouseIOFormRepository.InsertAsync(model);
                if (dto.WareHouseIOFormDts!=null)
                {
                    dto.WareHouseIOFormDts.ForEach(async c =>
                    {

                       var  WHDts= ObjectMapper.Map<WareHouseIOFormDts>(c);
                        WHDts.TicketNo = model.TicketNo;
                        WHDts.CreatorUserId = dto.WareHouseIOForm.CreatorUserId;
                        await  _wareHouseIOFormDtsRepository.InsertAsync(WHDts);
                    });
                }
                result.TicketNo = model.TicketNo;           
            }
            result.BaseResultDto.IsSuccess = true;
            return result;
        }
  

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <returns></returns>
        public async Task<List<WareHouseIOFormDtsDto>> GetWareHouseInfoDtsAsync(string ticketNo)
        {
            var query = (from a in _wareHouseIOFormDtsRepository.GetAll()
                         join user in _userManager.Users on a.CreatorUserId equals user.Id
                         select new WareHouseIOFormDtsDto
                         {
                             Id = a.Id,
                             TicketNo = a.TicketNo,
                             ItemBatchNo = a.ItemBatchNo,
                             LocationNo = a.LocationNo,
                             ProductSKU = a.ProductSKU,
                             Qty = a.Qty,
                             CreationTime = a.CreationTime,
                             CreatorUserId = a.CreatorUserId,
                             CreatorUserName = user.UserName

                         })
                      .WhereIf(!string.IsNullOrEmpty(ticketNo), c => c.TicketNo == ticketNo);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 列表页面删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateWareHouseIOFormAsync( UpdateWareHouseIOFormInput input)
        {
             await _wareHouseIOFormRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 详情页面删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateWareHouseIOFormDtsAsync(UpdateWareHouseIOFormInput input)
        {
            await _wareHouseIOFormDtsRepository.DeleteAsync(input.Id);
        }


        //public async Task<BaseResultDto> CommitToInspection(TicketNoDto dto)
        //{
        //    var result = new BaseResultDto();
        //    try
        //    {
        //        using (var unitOfwork = _unitOfWorkManager.Begin())
        //        {
        //            var WHIO = await _wareHouseIOFormRepository.FirstOrDefaultAsync(c => c.TicketNo == dto.TicketNo);
        //            if (WHIO==null)
        //            {
        //                result.IsSuccess = false;
        //                result.ErrorCode = 250;
        //                result.ErrorMessage = "单号在仓库中找不到！";
        //                return result;
        //            }
        //            if (WHIO.Status!=KGJEnum.WareHouseIOFormStatusEnum.New.GetHashCode())
        //            {
        //                result.IsSuccess = false;
        //                result.ErrorCode = 250;
        //                result.ErrorMessage = "单据不是新建状态！";
        //                return result;
        //            }
        //           var WHIODts= await _wareHouseIOFormDtsRepository.GetAllListAsync(c => c.TicketNo == WHIO.TicketNo);
        //            if (WHIODts.Count==0)
        //            {
        //                result.IsSuccess = false;
        //                result.ErrorCode = 250;
        //                result.ErrorMessage = "单据没有明细数据！";
        //                return result;
        //            }
        //            if (true)
        //            {

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

    }
}

/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ProductCustomFieldAppService.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Runtime.Session;
using KGJ.BaseDto;
using KGJ.ProductManagement.Dto;

namespace KGJ.ProductManagement
{
    public class ProductCustomFieldAppService : KGJAppServiceBase,IProductCustomFieldAppService
    {
        private readonly IRepository<ProductCustomField, long> _productCustomFieldRepository;

        public ProductCustomFieldAppService(
            IRepository<ProductCustomField,long> productCustomFieldRepository
            )
        {
            _productCustomFieldRepository = productCustomFieldRepository;
        }

        public async Task<BaseResultDto> CreateProductCustomFieldAsync(CreateProductCustomFieldInput input)
        {
            var result=new BaseResultDto();
            var count = _productCustomFieldRepository.GetAll().Where(p => p.CustomField == input.CustomField).Count();
            if (count > 0)
            {
                result.IsSuccess = false;
                result.ErrorCode = 250;
                result.ErrorMessage = "重复字段！";
                return result;
            }

            var entityProductCustomField = ObjectMapper.Map<ProductCustomField>(input);
            entityProductCustomField.TenantId = AbpSession.TenantId;
            entityProductCustomField.CreatorUserId = AbpSession.GetUserId();
            await _productCustomFieldRepository.InsertAsync(entityProductCustomField);

            result.IsSuccess = true;
            return result;

        }
        //public async Task<BaseResultDto> UpdateProductCustomFieldAsync()

        public async Task DeleteProductCustomFieldAsync(EntityDto<long> input)
        {
            var entity = await _productCustomFieldRepository.GetAsync(input.Id);
            entity.DeleterUserId = 8;

            //await _productCustomFieldRepository
        }


    }
}

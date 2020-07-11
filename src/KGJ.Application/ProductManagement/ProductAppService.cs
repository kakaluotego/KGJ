/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ProductAppService.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using KGJ.BaseDto;
using KGJ.ProductManagement.Dto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KGJ.ProductManagement
{
    public class ProductAppService : KGJAppServiceBase,IProductAppService
    {
        private readonly IRepository<Product, long> _productRepository;
        private readonly IRepository<ProductCustomField, long> _productCustomFieldRepository;
        private readonly IRepository<ProductAddFieldData, long> _productAddFieldDataRepository;

        public ProductAppService(
            IRepository<Product,long> productRepository,
            IRepository<ProductCustomField,long> productCustomFieldRepository,
            IRepository<ProductAddFieldData,long> productAddFieldDataRepository
            )
        {
            _productRepository = productRepository;
            _productCustomFieldRepository = productCustomFieldRepository;
            _productAddFieldDataRepository = productAddFieldDataRepository;
        }

        public async Task<BaseResultDto> CreateProductAsync(CreateProductInput input)
        {
            var result = new BaseResultDto();

            Product product = JsonConvert.DeserializeObject<Product>(input.Json);



            result.IsSuccess = true;
            return result;
            //var count = _productRepository.GetAll().Where(p => p.ProName == input.ProName).Count();
            //if (count > 0)
            //{
            //    result.IsSuccess = false;
            //    result.ErrorCode = 250;
            //    result.ErrorMessage = "重复字段！";
            //    return result;
            //}

            //var entityProduct = ObjectMapper.Map<Product>(input);
            //entityProduct.ProCode = "TTTTTTTTTTTTT";
            //entityProduct.TenantId = AbpSession.TenantId;
            //entityProduct.CreatorUserId = AbpSession.GetUserId();
            //entityProduct.IsValid = true;
            //await _productRepository.InsertAsync(entityProduct);

            //result.IsSuccess = true;
            //return result;

        }

        //public async Task<BaseResultDto> UpdateProductAsync(UpdateProductInput input)
        //{

        //}

        //public async Task DeleteProductAsync(EntityDto<long> input)
        //{

        //}

        //public async Task<PagedResultDto<ProductDto>> GetProductAsync()
        //{

        //}





    }
}

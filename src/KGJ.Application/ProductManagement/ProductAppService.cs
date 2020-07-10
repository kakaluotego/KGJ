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
using KGJ.BaseDto;
using KGJ.ProductManagement.Dto;
using Microsoft.EntityFrameworkCore;

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

        //public async Task<BaseResultDto> CreateProductAsync(CreateProductInput input)
        //{

        //}

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

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

using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using KGJ.Authorization.Users;
using KGJ.BaseDto;
using KGJ.ProductManagement.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using KGJ.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using KGJ.Configuration;

namespace KGJ.ProductManagement
{
    /// <summary>
    /// 产品自定义字段
    /// </summary>
    public class ProductCustomFieldAppService : KGJAppServiceBase,IProductCustomFieldAppService
    {
        private readonly IRepository<ProductCustomField, long> _productCustomFieldRepository;
        private readonly UserManager _userManager;
        private readonly IDbContextProvider<KGJDbContext> _dbContextProvider;
        private readonly IConfigurationRoot _appConfiguration;

        public ProductCustomFieldAppService(
            IRepository<ProductCustomField, long> productCustomFieldRepository,
            UserManager userManager,
            IDbContextProvider<KGJDbContext> dbContextProvider,
            IWebHostEnvironment env

        )
        {
            _productCustomFieldRepository = productCustomFieldRepository;
            _userManager = userManager;
            _dbContextProvider = dbContextProvider;
            _appConfiguration = env.GetAppConfiguration();
        }

        public async Task<int> TestAsync()
        {
            try
            {

                string sql = "ALTER TABLE ProductAddFieldDatas ADD {0} nvarchar(50) NULL";
                return _dbContextProvider.GetDbContext().Database.ExecuteSqlRaw(sql, "BoschMaterialNo");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BaseResultDto> CreateProductCustomFieldAsync(CreateProductCustomFieldInput input )
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

            string sql;
            if (input.IsRequired)
            {
                sql = $"ALTER TABLE ProductAddFieldDatas ADD {input.CustomField} nvarchar(50) NOT NULL";
            }
            else
            {
                sql = $"ALTER TABLE ProductAddFieldDatas ADD {input.CustomField} nvarchar(50) NULL";
            }

            _dbContextProvider.GetDbContext().Database.ExecuteSqlRaw(sql);

            var entityProductCustomField = ObjectMapper.Map<ProductCustomField>(input);
            entityProductCustomField.TenantId = AbpSession.TenantId;
            entityProductCustomField.CreatorUserId = AbpSession.GetUserId();
            entityProductCustomField.IsValid = true;
            await _productCustomFieldRepository.InsertAsync(entityProductCustomField);

            result.IsSuccess = true;
            return result;

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BaseResultDto> UpdateProductCustomFieldAsync(UpdateProductCustomFieldInput input)
        {
            var result=new BaseResultDto();
            //这里有并发性的问题
            var entity = await _productCustomFieldRepository.GetAsync(input.Id);
            entity.CustomField = input.CustomField;
            entity.IsRequired = input.IsRequired;
            await _productCustomFieldRepository.UpdateAsync(entity);

            result.IsSuccess = true;
            return result;
        }

        /// <summary>
        /// 删除（使无效）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateProductCustomFieldIsValidAsync(IsValidInput input)
        {
            var entity = await _productCustomFieldRepository.GetAsync(input.Id);
            entity.DeleterUserId = AbpSession.GetUserId();
            entity.DeletionTime=DateTime.Now;
            entity.IsValid = input.IsValid;

            await _productCustomFieldRepository.UpdateAsync(entity);
        }

        public async Task<PagedResultDto<ProductCustomFieldListDto>> GetProductCustomFieldAsync(GetProductCustomFieldInput input)
        {
            var query = from ps in _productCustomFieldRepository.GetAll()
                join user in _userManager.Users on ps.CreatorUserId.Value equals user.Id
                where (string.IsNullOrEmpty(input.Filter) ? 1 == 1 : ps.CustomField.Contains(input.Filter)) &&
                (input.IsValid.HasValue ? ps.IsValid==input.IsValid : 1==1)
                select new ProductCustomFieldListDto
                {
                    Id = ps.Id,
                    CustomField = ps.CustomField,
                    CreatorUserId = ps.CreatorUserId,
                    CreationTime = ps.CreationTime,
                    CreatorUserName = user.Surname + " " + user.Name,
                    IsRequired = ps.IsRequired,
                    IsValid = ps.IsValid
                };

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<ProductCustomFieldListDto>(totalCount, items);

        }
        /// <summary>
        /// 产品页面增加的字段
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<ProductCustomFieldDto>> GetCustomFieldAsync()
        {
            var query = await _productCustomFieldRepository.GetAllListAsync(p => p.IsValid);

            return new ListResultDto<ProductCustomFieldDto>(
                query.Select(p=>new ProductCustomFieldDto
                {
                    CustomField = p.CustomField,
                    IsRequired = p.IsRequired
                }).ToList());
        }

    }
}

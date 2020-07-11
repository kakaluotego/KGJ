/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ProductCustomFieldListDto.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;

namespace KGJ.ProductManagement.Dto
{
    public class ProductCustomFieldListDto : CreationAuditedEntityDto<long>
    {
        public string CustomField { get; set; }
        public bool IsRequired { get; set; }
        public string CreatorUserName { get; set; }
        public bool IsValid { get; set; }

    }
}

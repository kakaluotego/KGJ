/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：OrganizationUnitDto.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;


namespace KGJ.OrganizationUnits.Dto
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : AuditedEntityDto<long>
    {
        public long? ParentId { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public int MemberCount { get; set; }
        public bool Checked { get; set; }
    }
}

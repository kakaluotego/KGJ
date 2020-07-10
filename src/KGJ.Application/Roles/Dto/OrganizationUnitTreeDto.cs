/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：OrganizationUnitTreeDto.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;

namespace KGJ.Roles.Dto
{
    public class OrganizationUnitTreeDto : EntityDto<long>
    {
        public string Label { get; set; }
        public int MemberCount { get; set; }
        public List<OrganizationUnitTreeDto> Children { get; set; }
        public bool Checked { get; set; }
    }
}

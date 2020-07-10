/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：SystemCodeDto.cs
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
using Abp.Domain.Entities;

namespace KGJ.BasicManagement.Dto
{
    [AutoMap(typeof(SystemCode))]
    public class SystemCodeListDto : EntityDto<long?>
    {
        public long GroupId { get; set; }
        public int OrderNo { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public bool IsValid { get; set; }

    }
}

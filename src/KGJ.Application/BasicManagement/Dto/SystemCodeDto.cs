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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.AutoMapper;

namespace KGJ.BasicManagement.Dto
{
    [AutoMap(typeof(SystemCode))]
    public class SystemCodeDto
    {
        public int OrderNo { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public bool IsValid { get; set; }
    }
}

/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：CreateSystemCodeGroupInput.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KGJ.BasicManagement.Dto
{
    public class CreateSystemCodeGroupInput
    {
        [Required]
        public string GroupNo { get; set; }
        [Required]
        public string GroupName { get; set; }
        public string Desc { get; set; }
        [Required]
        public bool IsValid { get; set; }
        [Required]
        public long CreatorUserId { get; set; }
        public List<SystemCodeDto> SystemCodeDtos { get; set; }
    }
}

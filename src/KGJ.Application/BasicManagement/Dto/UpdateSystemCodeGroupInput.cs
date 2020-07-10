/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：UpdateSystemCodeGroupInput.cs
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
    public class UpdateSystemCodeGroupInput
    {
        public long Id { get; set; }
        public string GroupNo { get; set; }
        public string GroupName { get; set; }
        public string Desc { get; set; }
        public bool IsValid { get; set; }
        public long LastModifierUserId { get; set; }
        public List<SystemCodeListDto> SystemCodeListDtos { get; set; }

    }
}

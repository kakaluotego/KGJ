/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：SystemCode.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Domain.Entities;

namespace KGJ.BasicManagement
{
    [Table("SystemCodes")]
    public class SystemCode :Entity<long>,IMayHaveTenant
    {
        [Required]
        public long GroupId { get; set; }
        [StringLength(50)]
        [Required]
        public string Key { get; set; }
        [StringLength(50)]
        [Required]
        public string Value { get; set; }
        [StringLength(500)]
        public string Desc { get; set; }
        public int OrderNo { get; set; }
        public int? TenantId { get; set; }
        public bool IsValid { get; set; }
    }
}

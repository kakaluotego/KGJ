/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：SystemCodeGroup.cs
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
using Abp.Domain.Entities.Auditing;

namespace KGJ.BasicManagement
{
    [Table("SystemCodeGroups")]
    public class SystemCodeGroup :FullAuditedEntity<long>,IMayHaveTenant
    {
        [StringLength(50)]
        [Required]
        public string GroupNo { get; set; }
        [StringLength(50)]
        [Required]
        public string GroupName { get; set; }
        [StringLength(500)]
        public string Desc { get; set; }

        public int? TenantId { get; set; }
        public bool IsValid { get; set; }
    }
}

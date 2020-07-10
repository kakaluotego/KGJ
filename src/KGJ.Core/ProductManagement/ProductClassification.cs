/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ProductClassification.cs
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

namespace KGJ.ProductManagement
{
    [Table("ProductClassifications")]
    public class ProductClassification : FullAuditedEntity<long>, IMayHaveTenant
    {
        [StringLength(500)]
        [Required]
        public string Code { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public long? ParentId { get; set; }

        public int? TenantId { get; set; }
    }
}

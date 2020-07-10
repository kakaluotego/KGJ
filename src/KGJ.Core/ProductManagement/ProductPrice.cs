/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ProductPrice.cs
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
using Abp.Domain.Entities.Auditing;

namespace KGJ.ProductManagement
{
    public class ProductPrice :FullAuditedEntity<long>
    {
        [StringLength(50)]
        [Required]
        public string ProCode { get; set; }
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceNoTax { get; set; }
    }
}

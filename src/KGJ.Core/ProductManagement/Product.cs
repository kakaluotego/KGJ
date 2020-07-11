/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：Product.cs
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
using System.Runtime.CompilerServices;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace KGJ.ProductManagement
{
    [Table("Products")]
    public class Product :FullAuditedEntity<long>,IMayHaveTenant
    {
        [StringLength(50)]
        [Required]
        public string ProCode { get; set; }
        [StringLength(50)]
        [Required]
        public string ProName { get; set; }
        [StringLength(50)]
        public string ProAlias { get; set; }
        [StringLength(200)]
        public string ImagePath { get; set; }
        [StringLength(50)]
        public string ProClassification { get; set; }
        [StringLength(50)]
        [Required]
        public string ProModel { get; set; }
        [StringLength(50)]
        [Required]
        public string ProBrand { get; set; }
        [StringLength(50)]
        public string ProPackage { get; set; }
        [StringLength(50)]
        public string ProUnit { get; set; }
        [StringLength(500)]
        public string ProDesc { get; set; }
        [StringLength(50)]
        public string Size { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Weight { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Acreage { get; set; }
        [Required]
        public int MinNum { get; set; }
        public int? ChargeStandard { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ChargeAmount { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool IsGenerateQR { get; set; }
        public int? TenantId { get; set; }
        public bool IsValid { get; set; }
    }
}

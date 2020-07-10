/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ProductAddFieldData.cs
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

namespace KGJ.ProductManagement
{
    [Table("ProductAddFieldDatas")]
    public class ProductAddFieldData : Entity<long>
    {
        [Required]
        public long ProId { get; set; }
    }
}

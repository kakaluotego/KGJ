/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：GetProductCustomFieldInput.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KGJ.ProductManagement.Dto
{
    [AutoMap(typeof(ProductCustomField))]
    public class CreateProductCustomFieldInput
    {
        public string CustomField { get; set; }
        public bool IsRequired { get; set; }
    }
}

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

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Runtime.Validation;
using AutoMapper.Configuration;
using KGJ.BaseDto;

namespace KGJ.ProductManagement.Dto
{
    public class GetProductCustomFieldInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public bool? IsValid { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime Desc";
            }
        }
    }
}

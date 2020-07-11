/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ProductDto.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace KGJ.ProductManagement.Dto
{
    public class ProductDto
    {
        /// <summary>
        /// 产品别名
        /// </summary>
        public string ProAlias { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public string ProClassification { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string ProModel { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string ProBrand { get; set; }
        /// <summary>
        /// 包装
        /// </summary>
        public string ProPackage { get; set; }
        /// <summary>
        /// 基本单位
        /// </summary>
        public string ProUnit { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProDesc { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 面积(m²)
        /// </summary>
        public decimal Acreage { get; set; }
        /// <summary>
        /// 最小领用量
        /// </summary>
        public int MinNum { get; set; }
        /// <summary>
        /// 收费标准
        /// </summary>
        public int ChargeStandard { get; set; }
        /// <summary>
        /// 收费金额
        /// </summary>
        public decimal ChargeAmount { get; set; }
        /// <summary>
        /// 是否多单位
        /// </summary>
        public bool IsMultiUnit { get; set; }
        /// <summary>
        /// 产生二维码
        /// </summary>
        public bool IsGenerateQR { get; set; }

    }
}

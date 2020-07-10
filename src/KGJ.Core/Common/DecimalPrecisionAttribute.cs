/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：DecimalPrecisionAttribute.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace KGJ.Common
{
    [AttributeUsage(AttributeTargets.Property,Inherited = false,AllowMultiple = false)]
    public class DecimalPrecisionAttribute :Attribute
    {
        /// <summary>
        /// 精确度
        /// </summary>
        public byte Precision { get; set; }
        /// <summary>
        /// 小数保留位
        /// </summary>
        public byte Scale { get; set; }

        public DecimalPrecisionAttribute(byte precision,byte scale)
        {
            Precision = precision;
            Scale = scale;
        }


    }
}

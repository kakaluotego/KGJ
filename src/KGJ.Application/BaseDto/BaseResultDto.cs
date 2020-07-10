/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：BaseResultDto.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace KGJ.BaseDto
{
    public class BaseResultDto
    {
        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }

        public bool IsSuccess { get; set; }

        public string Remark { get; set; }

        public int SuccessCode { get; set; }
    }
}

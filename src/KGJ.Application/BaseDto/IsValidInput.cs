/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：IsValidInput.cs
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
    public class IsValidInput
    {
        public long Id { get; set; }

        public bool IsValid { get; set; }

        public long UserId { get; set; }
    }
}

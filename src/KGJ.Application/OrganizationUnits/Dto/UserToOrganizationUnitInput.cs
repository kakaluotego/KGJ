/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：UserToOrganizationUnitInput.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KGJ.OrganizationUnits.Dto
{
    public class UserToOrganizationUnitInput
    {
        [Range(1,long.MaxValue)]
        public long UserId { get; set; }
        [Range(1,long.MaxValue)]
        public long OrganizationUnitId { get; set; }
    }
}

/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：MoveOrganizationUnitInput.cs
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
    public class MoveOrganizationUnitInput
    {
        [Range(1,long.MaxValue)]
        public long Id { get; set; }
        public long? NewParentId { get; set; }
    }
}

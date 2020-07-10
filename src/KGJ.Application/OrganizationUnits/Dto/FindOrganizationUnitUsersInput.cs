/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：FindOrganizationUnitUsersInput.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using KGJ.BaseDto;

namespace KGJ.OrganizationUnits.Dto
{
    public class FindOrganizationUnitUsersInput :PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}

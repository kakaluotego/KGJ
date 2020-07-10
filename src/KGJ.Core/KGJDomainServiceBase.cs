﻿/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：KGJDomainServiceBase.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Services;

namespace KGJ
{
    public class KGJDomainServiceBase : DomainService
    {
        protected KGJDomainServiceBase()
        {
            LocalizationSourceName = KGJConsts.LocalizationSourceName;
        }
    }
}

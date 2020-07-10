/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：OrganizationUnitUserListDto.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using KGJ.Authorization.Users;

namespace KGJ.OrganizationUnits.Dto
{
    [AutoMap(typeof(User))]
    public class OrganizationUnitUserListDto :EntityDto<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public Guid? ProfilePictureId { get; set; }
        public DateTime AddedTime { get; set; }
    }
}

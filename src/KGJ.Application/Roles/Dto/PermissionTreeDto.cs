/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：PermissionTreeDto.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using Abp.Authorization;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using System.Collections.Generic;

namespace DBRS.Roles.Dto
{
    [AutoMap(typeof(Permission))]
    public class PermissionTreeDto
    {

        //标题
        public string Title { get; set; }

        //显示的名字
        public string DisplayName { get; set; }
        public string Name { get; set; }

        //是否展开直子节点
        public bool Expand { get; set; }

        //节点级别
        public int Level { get; set; }

        //是否选中节点
        public bool Selected { get; set; }

        // 是否勾选(如果勾选，子节点也会全部勾选)
        public bool Checked { get; set; }
        public MultiTenancySides MultiTenancySides { get; set; }     //是host  还是Tenant

        //子节点属性数组
        public List<PermissionTreeDto> Children { get; set; }
    }
}
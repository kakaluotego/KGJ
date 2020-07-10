/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：CommunicationProtocol.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Domain.Entities.Auditing;

namespace KGJ.MachineManagement
{
    public class CommunicationProtocol :FullAuditedEntity<long>
    {
        [StringLength(50)]
        [Required]
        public string Code { get; set; }
        [StringLength(50)]
        [Required]
        public string FactoryName { get; set; }
        [StringLength(50)]
        public string MachineModel { get; set; }
        [StringLength(50)]
        public string CommunicationCode { get; set; }
    }
}

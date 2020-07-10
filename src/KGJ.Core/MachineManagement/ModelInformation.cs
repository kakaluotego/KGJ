/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：ModelInformation.cs
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
    public class ModelInformation :FullAuditedEntity<long>
    {
        [MaxLength(50)]
        [Required]
        public string ModelName { get; set; }
        [StringLength(50)]
        [Required]
        public string ModelCode { get; set; }
        public int ContainerType { get; set; }
        [StringLength(50)]
        public string Manufacturers { get; set; }
        [StringLength(50)]
        public string ModelType { get; set; }
        public int CargoStartNo { get; set; }
        public int CargoNumber { get; set; }
        [StringLength(50)]
        public string ProtocolCode { get; set; }
        public int ModelRow { get; set; }
        public int ModelCol { get; set; }
    }
}

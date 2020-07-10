/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：Machine.cs
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
    public class Machine :FullAuditedEntity<long>
    {
        [StringLength(50)]
        [Required]
        public string MachineCode { get; set; }
        [StringLength(50)]
        [Required]
        public string MachineName { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }
        public int CardType { get; set; }
        [StringLength(50)]
        public string MachineModel { get; set; }
        public int ContainerType { get; set; }
        [StringLength(50)]
        public string Manufacturers { get; set; }
        public int CargoNumber { get; set; }
        public bool PlugIn { get; set; }
        [StringLength(50)]
        public string PlugInModelName { get; set; }
        public int PlugInNum { get; set; }
        public int PlugInCargoNum { get; set; }
        public int CyclesNumber { get; set; }
        public int Cycle { get; set; }
        public int ReplenishmentMethod { get; set; }
        [StringLength(50)]
        public string ReplenishmentWH { get; set; }
        public int Initialization { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public int MachineNum { get; set; }
        [StringLength(50)]
        public string VersionNo { get; set; }
    }
}

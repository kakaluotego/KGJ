using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KGJ.WareHouse
{
    [Table("WareHouseInfoDts")]
    public class WareHouseInfoDts : FullAuditedEntity<long>, IMayHaveTenant
    {
        // 库位编号
        public string LocationNo { get; set; }
        //容量
        public int Capacity { get; set; }
        // 已使用
        public int UsedCapacity { get; set; }
        public string WareHouseNo { get; set; }

        public int? TenantId { get; set; }
    }
}

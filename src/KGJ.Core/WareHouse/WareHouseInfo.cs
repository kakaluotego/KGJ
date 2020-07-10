using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KGJ.WareHouse
{
    [Table("WareHouseInfo")]
    public class WareHouseInfo : FullAuditedEntity<long>, IMayHaveTenant
    {
        public string WareHouseNo { get; set; }
        public string WareHouseName { get; set; }
        public string Desc { get; set; }
        // 仓库类型
        public int WHType { get; set; }
        //所属厂区
        public string SiteNo { get; set; }

        public int? TenantId { get; set; }
    }
}

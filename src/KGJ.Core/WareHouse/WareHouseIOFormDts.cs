using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace KGJ.WareHouse
{
    [Table("WareHouseIOFormDts")]
    public class WareHouseIOFormDts : FullAuditedEntity<long>, IMayHaveTenant
    {
        [MaxLength(50)]
        [Required]
        public string TicketNo { get; set; }   //单号
        [MaxLength(50)]
        public string ItemBatchNo { get; set; }   //物料批号
        public int Qty { get; set; }      // 数量  
        public string  ProductSKU { get; set; }  //唯一编号
        [MaxLength(50)]
        public string LocationNo { get; set; }     //  库位编号
        public int? TenantId { get; set; }

    }
}

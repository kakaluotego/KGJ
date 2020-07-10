using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace KGJ.WareHouse
{
    [Table("WareHouseIOForm")]
    public class WareHouseIOForm : FullAuditedEntity<long>, IMayHaveTenant
    {
        [MaxLength(50)]
        [Required]
        public string TicketNo { get; set; }           //单号
        [MaxLength(50)]
        [Required]
        public string WareHouseNo { get; set; }            // 仓库编码
        [MaxLength(50)]
        public string SupplierNo { get; set; }    //供应商编号
        [MaxLength(50)]
        public string CustomerNo { get; set; }    //客户编号
        [MaxLength(500)]
        public string Remark { get; set; }        // 描述
        public int Status { get; set; }        // 状态


        //1.进料  2.领料
        [MaxLength(50)]
        public string IOType { get; set; }   //出入类型

        public int? TenantId { get; set; }

    }
}

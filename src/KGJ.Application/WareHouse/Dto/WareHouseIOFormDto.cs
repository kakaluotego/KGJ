using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace KGJ.WareHouse.Dto
{
    [AutoMap(typeof(WareHouseIOForm))]
    public class WareHouseIOFormDto : FullAuditedEntity<long?>
    {
        public string TicketNo { get; set; }
        public string WareHouseNo { get; set; }
        public string WareHouseName { get; set; }
        public string SupplierNo { get; set; }
        public string SupplierName { get; set; }
        //public string CustomerNo { get; set; }
        //public string CusTomerName { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string IOType { get; set; } 
        public string CreatorUserName { get; set; }

        public int Qty { get; set; }
    



    }
}

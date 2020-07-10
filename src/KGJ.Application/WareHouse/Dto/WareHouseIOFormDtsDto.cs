using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities.Auditing;
using Abp.AutoMapper;

namespace KGJ.WareHouse.Dto
{
    [AutoMap(typeof(WareHouseIOFormDts))]
    public class WareHouseIOFormDtsDto : FullAuditedEntity<long?>
    {
        public string TicketNo { get; set; }
        public string ProductSKU { get; set; }
        public int Qty { get; set; }
        public string ItemBatchNo { get; set; }
        public string LocationNo { get; set; }
        public string CreatorUserName { get; set; }
    }    
}

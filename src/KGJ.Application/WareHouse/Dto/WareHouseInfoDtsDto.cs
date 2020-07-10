using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.WareHouse.Dto
{
    [AutoMap(typeof(WareHouseInfoDts))]
    public class WareHouseInfoDtsDto : FullAuditedEntity<long?>
    {
        public string LocationNo { get; set; }
        public int Capacity { get; set; }
        public int UsedCapacity { get; set; }
        public string WareHouseNo { get; set; }
        public string CreatorUserName { get; set; }
      
    }
}

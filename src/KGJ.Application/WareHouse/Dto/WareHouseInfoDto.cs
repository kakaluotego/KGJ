using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.WareHouse.Dto
{
    [AutoMap(typeof(WareHouseInfo))]
    public class WareHouseInfoDto : FullAuditedEntity<long?>
    {
        public string WareHouseNo { get; set; }
        public string WareHouseName { get; set; }
        public string Desc { get; set; }
        public int WHType { get; set; }
        public string WHTypeName { get; set; }
        //public string SiteNo { get; set; }
        public string CreatorUserName { get; set; }
       
    }
}

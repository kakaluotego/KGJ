using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.WareHouse.Dto
{
    public class WareHouseInfoCreateDto
    {
        public WareHouseInfoDto WareHouseInfo { get; set; }
        public List<WareHouseInfoDtsDto> WareHouseInfoDts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.WareHouse.Dto
{
    public class WareHouseIOFormCreateDto
    {
        public WareHouseIOFormDto WareHouseIOForm { get; set; }
        public List<WareHouseIOFormDtsDto> WareHouseIOFormDts { get; set; }
    }
}

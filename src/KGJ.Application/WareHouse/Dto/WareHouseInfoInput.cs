using Abp.Runtime.Validation;
using KGJ.BaseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.WareHouse.Dto
{
        public class WareHouseInfoInput : PagedAndSortedInputDto, IShouldNormalize
        {
            public string Filter { get; set; }
            public void Normalize()
            {
                if (string.IsNullOrEmpty(Sorting))
                {
                    Sorting = "CreationTime Desc";
                }
            }
        }
    
}

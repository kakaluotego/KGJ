using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Runtime.Validation;
using KGJ.BaseDto;

namespace KGJ.WareHouse.Dto
{
    public class WareHouseIOFormInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string IOType { get; set; }
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

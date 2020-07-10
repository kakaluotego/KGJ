using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ
{
    public class KGJEnum
    {
        public enum WareHouseIOTypeEnum
        {
            RawMaterialIn = 1,
            RawMaterialOut = 2,
            RawMaterialBack = 3,
            WareHouseIn = 11,
            WareHouseOut = 12
        }
        public enum WareHouseIOFormStatusEnum
        {
            New = 0,
            PendingInspection = 1,
            PendingInput = 2,
            Commited = 10,
            Closed = 12,
            Back = -1

        }
       
    }
}

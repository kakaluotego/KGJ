using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.WareHouse
{
    public  class DeleteWareHouseInfoInput
    {
        public long Id { get; set; }

        public string wareHouseNo { get; set; }

        public long UserId { get; set; }
    }
}

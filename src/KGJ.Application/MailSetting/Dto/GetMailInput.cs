using Abp.Runtime.Validation;
using KGJ.BaseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.MailSetting.Dto
{
  public  class GetMailInput: PagedAndSortedInputDto, IShouldNormalize
    {
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime desc";
            }
        }
    }
}

using Abp.Runtime.Validation;
using KGJ.BaseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.AuditLogs.Dto
{
    public class GetAuditLogInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string UserName { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string Service { get; set; }

        public string MethodName { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ExecutionTime desc";
            }
        }
    }
}

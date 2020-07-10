using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KGJ.MailSetting
{
    [Table("MailSubscribe")]
    public class MailSubscribe : FullAuditedEntity<long>, IMayHaveTenant
    {
        public DateTime StartTime { get; set; }

        public int IntervalDays { get; set; }

        public string EmailAddress { get; set; }

        public string Remark { get; set; }

        public string EmailType { get; set; }

        public int? TenantId { get; set; }
    }
}

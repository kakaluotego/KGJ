using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.MailSetting.Dto
{
    public class MailSubscribeDto : FullAuditedEntity<long>
    {

        public DateTime StartTime { get; set; }

        public int IntervalDays { get; set; }

        public string EmailAddress { get; set; }

        public string Remark { get; set; }

        public string EmailType { get; set; }

        public string EmailTypeDesc { get; set; }

        public int? TenantId { get; set; }

        public string CreatorUserName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.AuditLogs.Dto
{
    public class AuditLogDto
    {
        //
        // 摘要:
        //     Start time of the method execution.
        public virtual DateTime ExecutionTime { get; set; }
        //
        // 摘要:
        //     Abp.Auditing.AuditInfo.ImpersonatorUserId.
        public virtual long? ImpersonatorUserId { get; set; }
        //
        // 摘要:
        //     Exception object, if an exception occured during execution of the method.
        public virtual string Exception { get; set; }
        //
        // 摘要:
        //     Browser information if this method is called in a web request.
        public virtual string BrowserInfo { get; set; }
        //
        // 摘要:
        //     Name (generally computer name) of the client.
        public virtual string ClientName { get; set; }
        //
        // 摘要:
        //     IP address of the client.
        public virtual string ClientIpAddress { get; set; }
        //
        // 摘要:
        //     Total duration of the method call as milliseconds.
        public virtual int ExecutionDuration { get; set; }
        //
        // 摘要:
        //     Return values.
        public virtual string ReturnValue { get; set; }
        //
        // 摘要:
        //     TenantId.
        public virtual int? TenantId { get; set; }
        //
        // 摘要:
        //     Executed method name.
        public virtual string MethodName { get; set; }
        //
        // 摘要:
        //     Service (class/interface) name.
        public virtual string ServiceName { get; set; }
        //
        // 摘要:
        //     UserId.
        public virtual long? UserId { get; set; }
        //
        // 摘要:
        //     Abp.Auditing.AuditInfo.ImpersonatorTenantId.
        public virtual int? ImpersonatorTenantId { get; set; }
        //
        // 摘要:
        //     Calling parameters.
        public virtual string Parameters { get; set; }
        //
        // 摘要:
        //     Abp.Auditing.AuditInfo.CustomData.
        public virtual string CustomData { get; set; }

        public string CreatorUserName { get; set; }

        public long Id { get; set; }
    }
}

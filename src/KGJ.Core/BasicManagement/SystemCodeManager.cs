/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：SystemCodeManager.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;

namespace KGJ.BasicManagement
{
    public class SystemCodeManager : KGJDomainServiceBase
    {
        private readonly IRepository<SystemCodeGroup, long> _systemCodeGroupRepository;
        private readonly IRepository<SystemCode, long> _systemCodeRepository;

        public SystemCodeManager(
            IRepository<SystemCodeGroup,long> systemCodeGroupRepository,
            IRepository<SystemCode,long> systemCodeRepository
            )
        {
            _systemCodeGroupRepository = systemCodeGroupRepository;
            _systemCodeRepository = systemCodeRepository;
        }

        public virtual IQueryable<KeyValue> CardType
        {
            get
            {
                var query = from codeGroup in _systemCodeGroupRepository.GetAll()
                    join code in _systemCodeRepository.GetAll() on codeGroup.Id equals code.GroupId
                    where codeGroup.IsValid && code.IsValid && codeGroup.GroupName == "SYSCODE_CARD_TYPE"
                    select new KeyValue
                    {
                        Key = code.Key,
                        Value = code.Value
                    };

                return query;
            }
        }

    }
}

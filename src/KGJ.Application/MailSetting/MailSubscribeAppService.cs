using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using KGJ.Authorization.Users;
using KGJ.MailSetting.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using KGJ.BaseDto;
using Abp.Authorization;
using KGJ.Authorization;

namespace KGJ.MailSetting
{
    [AbpAuthorize(PermissionNames.Pages_MailSubscribe)]
    public class MailSubscribeAppService : KGJAppServiceBase
    {
        private readonly IRepository<MailSubscribe, long> _mailSubscribeRepository;
        private readonly IRepository<User, long> _userRepository;

        public MailSubscribeAppService(IRepository<MailSubscribe, long> mailSubscribeRepository, IRepository<User, long> userRepository)
        {
            _userRepository = userRepository;
            _mailSubscribeRepository = mailSubscribeRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_MailSubscribe)]
        public async Task<PagedResultDto<MailSubscribeDto>> GetEmailSubscribeAsync(GetMailInput input)
        {
            var query = from mail in _mailSubscribeRepository.GetAll()
                        join user in _userRepository.GetAll() on mail.CreatorUserId equals user.Id
                        select new MailSubscribeDto
                        {
                            Id = mail.Id,
                            StartTime = mail.StartTime,
                            IntervalDays = mail.IntervalDays,
                            EmailAddress = mail.EmailAddress,
                            Remark = mail.Remark,
                            EmailType = mail.EmailType,
                            TenantId = mail.TenantId,
                            CreatorUserName = user.UserName,
                            CreationTime = mail.CreationTime
                        };

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<MailSubscribeDto>(totalCount, items);
        }

        [AbpAuthorize(PermissionNames.Pages_MailSubscribe_Create)]
        public async Task<BaseResultDto> CreateAsync(MailSubscribeDto dto)
        {
            var result = new BaseResultDto();
            if (dto.Id > 0)
            {
                var model = await _mailSubscribeRepository.GetAsync(dto.Id);
                model.StartTime = dto.StartTime.ToLocalTime();
                model.IntervalDays = dto.IntervalDays;
                model.EmailAddress = dto.EmailAddress;
                model.Remark = dto.Remark;
                await _mailSubscribeRepository.UpdateAsync(model);
            }
            else
            {
                var check = _mailSubscribeRepository.GetAll().Where(p => p.EmailType == dto.EmailType);
                if (check.Count() > 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = L("ThisTypeOfEmailReminderAlreadyExists");
                    return result;
                }

                var model = ObjectMapper.Map<MailSubscribe>(dto);
                model.StartTime = model.StartTime.ToLocalTime();
                model.CreatorUserId = AbpSession.UserId;
                model.TenantId = AbpSession.TenantId;

                await _mailSubscribeRepository.InsertAsync(model);
            }

            result.IsSuccess = true;
            return result;
        }

        [AbpAuthorize(PermissionNames.Pages_MailSubscribe_Edit)]
        public async Task<BaseResultDto> UpdateAsync(MailSubscribeDto dto)
        {
            var result = new BaseResultDto();
            if (dto.Id > 0)
            {
                var model = await _mailSubscribeRepository.GetAsync(dto.Id);
                model.StartTime = dto.StartTime.ToLocalTime();
                model.IntervalDays = dto.IntervalDays;
                model.EmailAddress = dto.EmailAddress;
                model.Remark = dto.Remark;
                await _mailSubscribeRepository.UpdateAsync(model);
            }
            else
            {
                var check = _mailSubscribeRepository.GetAll().Where(p => p.EmailType == dto.EmailType);
                if (check.Count() > 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = L("ThisTypeOfEmailReminderAlreadyExists");
                    return result;
                }

                var model = ObjectMapper.Map<MailSubscribe>(dto);
                model.StartTime = model.StartTime.ToLocalTime();
                model.CreatorUserId = AbpSession.UserId;
                model.TenantId = AbpSession.TenantId;

                await _mailSubscribeRepository.InsertAsync(model);
            }

            result.IsSuccess = true;
            return result;
        }

        [AbpAuthorize(PermissionNames.Pages_MailSubscribe_Delete)]
        public async Task DeleteAsync(long id)
        {
            await _mailSubscribeRepository.DeleteAsync(id);
        }
    }
}

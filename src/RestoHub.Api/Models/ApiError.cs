﻿using FluentValidator;
using RestoHub.Api.Modules.Shared.Application.Notifications;
using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class ApiError
    {
        public ErrorCode? Code { get; set; }
        public ICollection<ErrorData> Errors { get; set; } = new List<ErrorData>();

        public ApiError()
        {
        }

        public ApiError(ICollection<Notification> errors, ErrorCode? error = null)
        {
            Code = error;
            Errors = errors.Select(x => (ErrorData)x).ToList();
        }

        public static ApiError FromResult(Result result)
        {
            return new ApiError(result.Notifications.ToList(), result.Error);
        }
    }
}

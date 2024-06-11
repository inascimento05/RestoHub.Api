using Microsoft.AspNetCore.Mvc;
using RestoHub.Api.Modules.Shared.Application.Notifications;

namespace RestoHub.Api.Presenters
{
    public interface IPresenter
    {
        IActionResult GetResult<T>(DataResult<T> result);
    }
}

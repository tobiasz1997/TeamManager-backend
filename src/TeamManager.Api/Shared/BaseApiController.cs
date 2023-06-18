using Microsoft.AspNetCore.Mvc;

namespace TeamManager.Api.Shared;

public class BaseApiController : ControllerBase
{
    protected Guid UserId => Guid.Parse(GetUserId());

    private string GetUserId()
    {
        if (string.IsNullOrWhiteSpace(HttpContext.User.Identity?.Name))
        {
            throw new AuthorizationException();
        }

        return HttpContext.User.Identity.Name;
    }
}
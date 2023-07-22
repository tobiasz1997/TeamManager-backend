using Microsoft.Extensions.DependencyInjection;

namespace TeamManager.Common.AspNet.BehaviourOptions;

public static class BehaviourOptionsExtension
{
    public static IMvcBuilder AddBehaviourOptionsExtension(this IMvcBuilder service)
    {
        service.ConfigureApiBehaviorOptions(options => 
            {
                options.InvalidModelStateResponseFactory =
                    ModelStateValidator.ValidateModelState;
            });
        return service;
    }
}
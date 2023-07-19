using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Common.AspNet.BehaviourOptions;

internal static class ModelStateValidator
{
    private const string ErrorType = "data_annotation_error";
    public static IActionResult ValidateModelState(ActionContext context)
    {
        (_, ModelStateEntry entry) = context.ModelState
            .First(x => x.Value.Errors.Count > 0);
        string errorSerialized = entry.Errors.First().ErrorMessage;
        
        ErrorResponse error = new ErrorResponse(ErrorType, errorSerialized);
        var result = new BadRequestObjectResult(error);

        return result;
    }
}
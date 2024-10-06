using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityServer.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState, List<string> errors)
        {
            errors.ForEach(e => modelState.AddModelError(string.Empty, e));
        }

        public static void AddModelErrorList(this ModelStateDictionary modelState,IEnumerable<IdentityError> errors)
        {
            errors.ToList().ForEach(e => modelState.AddModelError(string.Empty, e.Description));
        }
    }
}

using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors= new List<IdentityError>();

            var isNumeric = int.TryParse(user.UserName, out _);
            if (isNumeric)
            {
                errors.Add(new()
                {
                    Code = "UserNameCannotBeContainFirstLetterDigit",
                    Description = "Kullanıcı adının ilk karakteri sayı olmamalıdır"
                });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace IdentityServer.Localization
{
    //Türkçeleştirmek için override yazıp yapabilirsin
    public class LocalizationIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DublicateUserName", Description = $"Bu {userName} daha önce başka bir kullanıcı tarafından kullanılmıştır." };
            //return base.DuplicateUserName(userName);
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DublicateEmail", Description = $"Bu {email} daha önce başka bir kullanıcı tarafından kullanılmıştır." };

            //return base.DuplicateEmail(email);
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Şifre en az 6 karakterli olmalıdır." };
            //return base.PasswordTooShort(length);
        }
    }
}

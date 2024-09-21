using IdentityServer.CustomValidations;
using IdentityServer.Localization;
using IdentityServer.Models;

namespace IdentityServer.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExtension(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = "abcdefgijklmoprstuvwxyz123456789_"; // izin verilen karakterleri ayarlayabilirsin
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                //Lockout işlemleri

                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(3);// kaç dk kitlensin
                options.Lockout.MaxFailedAccessAttempts = 3;//kaçıncı da kitlensin

            }).AddPasswordValidator<PasswordValidator>()
            .AddUserValidator<UserValidator>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
            .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
            
        }
        public SignInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
        [EmailAddress(ErrorMessage = "Email Formatı Yanlıştır!")]
        [Required(ErrorMessage = "Mail Boş Geçilemez!")]
        [Display(Name = "Mail :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Boş Geçilemez!")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla :")]
        public bool RememberMe { get; set; }
    }
}

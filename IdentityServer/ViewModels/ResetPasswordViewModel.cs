using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Geçilemez!")]
        [Display(Name = "Yeni Şifre :")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmamaktadır.")]
        [Required(ErrorMessage = "Şifre Tekrar Boş Geçilemez!")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}

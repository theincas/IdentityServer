using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Email Formatı Yanlıştır!")]
        [Required(ErrorMessage = "Mail Boş Geçilemez!")]
        [Display(Name = "Mail :")]
        public string Email { get; set; }
    }
}

using IdentityServer.Enums;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez!")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Email Formatı Yanlıştır!")]
        [Required(ErrorMessage = "Mail Boş Geçilemez!")]
        [Display(Name = "Mail :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Boş Geçilemez!")]
        [Display(Name = "Telefon :")]
        public string Phone { get; set; }

        [Display(Name = "Şehir :")]
        public string? City { get; set; }

        [Display(Name = "Profil Resmi :")]
        public IFormFile? Picture { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi :")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Cinsiyet :")]
        public Gender? Gender { get; set; }
    }
}

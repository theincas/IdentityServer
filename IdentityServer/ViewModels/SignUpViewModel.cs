using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        { }
        public SignUpViewModel(string userName, string email, string phone, string password)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }
        //[Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez!")]
        //[Display(Name ="Kullanıcı Adı :")]
        public string UserName { get; set; }

        //[EmailAddress(ErrorMessage ="Email Formatı Yanlıştır!")]
        //[Required(ErrorMessage = "Mail Boş Geçilemez!")]
        //[Display(Name = "Mail :")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Telefon Boş Geçilemez!")]
        //[Display(Name = "Telefon :")]
        public string Phone { get; set; }


        //[Required(ErrorMessage = "Şifre Boş Geçilemez!")]
        //[Display(Name = "Şifre :")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string Password { get; set; }

        //[Compare(nameof(Password),ErrorMessage ="Şifreler uyuşmamaktadır.")]
        //[Required(ErrorMessage = "Şifre Tekrar Boş Geçilemez!")]
        //[Display(Name = "Şifre Tekrar :")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string PasswordConfirm { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Rol Adı Boş Geçilemez!")]
        [Display(Name = "Rol Adı :")]
        [MinLength(3, ErrorMessage = "Rol adı en az 3 karakter olabilir")]
        public string Name { get; set; }
    }
}

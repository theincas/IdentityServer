using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Areas.Admin.Models
{
    public class RoleUpdateViewModel
    {
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Rol Adı Boş Geçilemez!")]
        [Display(Name = "Rol Adı :")]
        [MinLength(3, ErrorMessage = "Rol adı en az 3 karakter olabilir")]
        public string Name { get; set; }
    }
}

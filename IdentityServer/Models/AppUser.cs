using IdentityServer.Enums;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    public class AppUser:IdentityUser
    {
        //İhtiyacın olan alanları tanımlayabilirsin
        //default olarak IdentityServer'ın sunduğu db'yi kullanmak istiyorsan ekleme yapmana gerek kalmayacaktır.

        public string? City { get; set; }
        public string? Picture { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MS.CoachSystem.Web.ViewModels
{
    public class CreateCoachViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şehir zorunludur")]
        [Display(Name = "Şehir")]
        public string City { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur")]
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "TCKN zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TCKN 11 karakter olmalıdır")]
        [Display(Name = "TCKN")]
        public string TCKN { get; set; }

        [Required(ErrorMessage = "Cinsiyet zorunludur")]
        [Display(Name = "Cinsiyet")]
        public int Gender { get; set; }
    }
}

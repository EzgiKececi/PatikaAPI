using System.ComponentModel.DataAnnotations;

namespace PatikaAPI.Entities
{
    public class CrazyMusicianEntity
    {
        [Required (ErrorMessage = "Veriler Id ile girilmek zorundadır.")]
        public int Id { get; set; }

        [Required(ErrorMessage ="İsim girmek zorunludur.")]
        [MinLength (2, ErrorMessage ="En az iki karakterden oluşmak zorundadır.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "İsim girmek zorunludur.")]
        [MinLength(2, ErrorMessage = "En az iki karakterden oluşmak zorundadır.")]
        public string LastName { get; set; }

        public string Job { get; set; }
        public string FunnyTrait { get; set; }
        public bool IsDeleted { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace KGJ.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
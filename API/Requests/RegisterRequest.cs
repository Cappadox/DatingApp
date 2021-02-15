using System.ComponentModel.DataAnnotations;

namespace API.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength=4)]
        public string Password {get; set;}
    }
}
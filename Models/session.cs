using System.ComponentModel.DataAnnotations;

namespace Project_API_Midursan_Velusamy_2031313.Models
{
    public class session
    {
        //Attributes: 
        [Key]
        public string Token { get; set; }

        [Required]
        public string Email { get; set; }

        //Constructor:
        public session() {
            Token = Guid.NewGuid().ToString();
        }
    }
}

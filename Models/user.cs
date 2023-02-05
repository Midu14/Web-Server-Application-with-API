using System.ComponentModel.DataAnnotations;

namespace Project_API_Midursan_Velusamy_2031313.Models
{
    public class user
    {
        //Attributes: 
        [Key]
        public string UId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        //Constructor:
        public user() {
            UId = Guid.NewGuid().ToString();
        }
    }
}





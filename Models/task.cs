using System.ComponentModel.DataAnnotations;

namespace Project_API_Midursan_Velusamy_2031313.Models
{
    public class task
    {
        //Attributes: 
        [Key]
        public string TaskUId { get; set; }

        [Required]
        public string CreatedByUid { get; set; }

        [Required]
        public string CreatedByName { get; set; }

        [Required]
        public string AssignedToUid { get; set; }

        [Required]
        public string AssignedToName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool Done { get; set; }

        //Contructor
        public task()
        {
            TaskUId = Guid.NewGuid().ToString();
            Done = false;
        }
    }
}

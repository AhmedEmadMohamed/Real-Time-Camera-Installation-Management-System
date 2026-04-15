using System.ComponentModel.DataAnnotations;

namespace Real_Time_Camera_Installation_Management_System.Models.DTOs
{
    public class CreateJobDTO
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string JobDescription { get; set; }
    }
}

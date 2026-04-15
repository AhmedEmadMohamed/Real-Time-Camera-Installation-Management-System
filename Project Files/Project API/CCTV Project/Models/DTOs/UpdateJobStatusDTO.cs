using System.ComponentModel.DataAnnotations;

namespace Real_Time_Camera_Installation_Management_System.Models.DTOs
{
    public class UpdateJobStatusDTO
    {
        [Required]
        [RegularExpression("^(Pending|In Progress|Completed|Cancelled)$",
        ErrorMessage = "Status must be: Pending, In Progress, Completed, or Cancelled.")]
        public string JobStatus { get; set; }

    }
}

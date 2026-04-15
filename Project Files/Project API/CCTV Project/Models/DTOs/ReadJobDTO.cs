using Real_Time_Camera_Installation_Management_System.Models.Domain;

namespace Real_Time_Camera_Installation_Management_System.Models.DTOs
{
    public class ReadJobDTO
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string CustomerName { get; set; } 
        public string JobDescription { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string JobStatus { get; set; }
    }
}

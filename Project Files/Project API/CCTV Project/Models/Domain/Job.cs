namespace Real_Time_Camera_Installation_Management_System.Models.Domain
{
    public class Job
    { 
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; } //FK
        public string JobDescription { get; set; }
        public DateTime ScheduledDate { get; set; } = DateTime.Now;
        public string JobStatus { get; set; }

    }
}

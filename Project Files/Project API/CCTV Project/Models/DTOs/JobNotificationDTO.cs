namespace Real_Time_Camera_Installation_Management_System.Models.DTOs
{
    public class JobNotificationDTO
    {
        public int JobId { get; set; }
        public string CustomerName { get; set; }
        public string NewStatus { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

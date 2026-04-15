namespace Real_Time_Camera_Installation_Management_System.Models.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; } 
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; } 
        public ICollection<Job> Job { get; set; }
    }
}

using Real_Time_Camera_Installation_Management_System.Models.Domain;

namespace Real_Time_Camera_Installation_Management_System.Models.DTOs
{
    public class ReadCustomerDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
    }


}

using System.ComponentModel.DataAnnotations;

namespace Real_Time_Camera_Installation_Management_System.Models.DTOs
{
    public class CreateCustomerDTO
    {
        [Required]
        public string CustomerName { get; set; }
        [Required, RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid Egyptian phone number.")]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
    }
}

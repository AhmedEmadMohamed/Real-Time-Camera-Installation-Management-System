using Real_Time_Camera_Installation_Management_System.Models.Domain;

namespace Real_Time_Camera_Installation_Management_System.Repos.Interfaces
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> CreateAsync(Customer customer);
    }
}

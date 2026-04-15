using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Real_Time_Camera_Installation_Management_System.Data;
using Real_Time_Camera_Installation_Management_System.Models.Domain;
using Real_Time_Camera_Installation_Management_System.Repos.Interfaces;

namespace Real_Time_Camera_Installation_Management_System.Repos.Implmentations
{
    public class CustomerRepo:ICustomerRepo
    {
        private readonly ProjectDbContext _projectDbContext;
        public CustomerRepo(ProjectDbContext projectDbContext) {
            this._projectDbContext = projectDbContext;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _projectDbContext.AddAsync(customer);
            await _projectDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _projectDbContext.Customers.Include(i=>i.Job).ToListAsync();
        }


    }
}

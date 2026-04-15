using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Real_Time_Camera_Installation_Management_System.Data;
using Real_Time_Camera_Installation_Management_System.Models.Domain;
using Real_Time_Camera_Installation_Management_System.Repos.Interfaces;

namespace Real_Time_Camera_Installation_Management_System.Repos.Implmentations
{
    public class JobRepo : IJobRepo
    {
        private readonly ProjectDbContext _projectDbContext;
        public JobRepo(ProjectDbContext projectDbContext)
        {
            this._projectDbContext = projectDbContext;
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            await _projectDbContext.AddAsync(job);
            await _projectDbContext.SaveChangesAsync();
            return job;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync(string? status = null)
        {
            IQueryable<Job> query = _projectDbContext.Jobs.Include(j=>j.Customer);
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(j=>j.JobStatus == status);
            }

            return await query.ToListAsync();
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            
            return await _projectDbContext.Jobs.Include(j=>j.Customer).FirstOrDefaultAsync(j=>j.JobId == id);
        }

        public async Task<bool> UpdateJobStatus(int id, string newStatus)
        {
            var job = await _projectDbContext.Jobs.FindAsync(id);
            if (job == null) { 
                return false;            
            }
            job.JobStatus = newStatus;
            await _projectDbContext.SaveChangesAsync();
            return true;
        }
    }
}

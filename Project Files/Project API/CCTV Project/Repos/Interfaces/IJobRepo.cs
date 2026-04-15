using Real_Time_Camera_Installation_Management_System.Models.Domain;

namespace Real_Time_Camera_Installation_Management_System.Repos.Interfaces
{
    public interface IJobRepo
    {
        Task<Job> CreateJobAsync(Job job);
        Task<IEnumerable<Job>> GetAllJobsAsync(string? status=null);
        Task<Job> GetByIdAsync(int id);
        Task<bool> UpdateJobStatus(int id, string newStatus);

    }
}

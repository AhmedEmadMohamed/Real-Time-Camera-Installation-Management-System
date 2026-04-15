using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Real_Time_Camera_Installation_Management_System.Hubs;
using Real_Time_Camera_Installation_Management_System.Models.Domain;
using Real_Time_Camera_Installation_Management_System.Models.DTOs;
using Real_Time_Camera_Installation_Management_System.Repos.Interfaces;

namespace Real_Time_Camera_Installation_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepo _jobRepo;
        private readonly IHubContext<JobHub> _hubContext;
        public JobsController(IJobRepo jobRepo, IHubContext<JobHub> hubContext)
        {
            this._jobRepo = jobRepo;
            this._hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDTO request)
        {
            var job = new Job
            {
                JobTitle = request.JobTitle,
                CustomerId = request.CustomerId,
                JobDescription = request.JobDescription
            };
            await _jobRepo.CreateJobAsync(job);
            return Ok(new { message = "Job Created Successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? status)
        {
            var jobs = await _jobRepo.GetAllJobsAsync(status);
            var response = new List<ReadJobDTO>();
            foreach (var job in jobs) {
                response.Add(new ReadJobDTO {
                    JobId=job.JobId,
                    JobTitle=job.JobTitle,
                    CustomerName = job.Customer.CustomerName,
                    JobDescription=job.JobDescription,
                    ScheduledDate=job.ScheduledDate,
                    JobStatus=job.JobStatus

                });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobRepo.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound(new { message = $"Job with ID {id} not found" });
            }

            var response = new ReadJobDTO
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                CustomerName = job.Customer.CustomerName,
                JobDescription = job.JobDescription,
                ScheduledDate = job.ScheduledDate,
                JobStatus = job.JobStatus
            };
            return Ok(response);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody]UpdateJobStatusDTO request)
        {
            var isUpdated = await _jobRepo.UpdateJobStatus(id, request.JobStatus);
            if (!isUpdated)
            {
                return NotFound($"Job with ID {id} was not found.");
            }

            var job = await _jobRepo.GetByIdAsync(id);
            var notificationObject = new
            {
                JobId = id,
                CustomerName = job.Customer.CustomerName,
                JobDescription = job.JobDescription,
                NewStatus = job.JobStatus,
                TimeStamp = DateTime.Now
            };
            await _hubContext.Clients.All.SendAsync("ReceiveJobUpdate", notificationObject);

            return Ok();
        }
    }
}

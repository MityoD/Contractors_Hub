using ContractorsHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractorsHub.Data.Configuration
{
    public class JobStatusConfiguration : IEntityTypeConfiguration<JobStatus>
    {
        public void Configure(EntityTypeBuilder<JobStatus> builder)
        {
            builder.HasData(JobStatusUpdate());
        }

        private List<JobStatus> JobStatusUpdate()
        {
            var jobStatus = new List<JobStatus>();
            //pending
            var status = new JobStatus()
            {
                Id = 1,
                Name = "Pending"
            };

            jobStatus.Add(status);
            //approved
            status = new JobStatus()
            {
                Id = 2,
                Name = "Approved"
            };

            jobStatus.Add(status);
            //declined
            status = new JobStatus()
            {
                Id = 3,
                Name = "Declined"
            };

            jobStatus.Add(status);

            return jobStatus;
        }
    }
}

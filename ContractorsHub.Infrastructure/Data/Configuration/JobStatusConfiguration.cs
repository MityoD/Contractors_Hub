using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractorsHub.Infrastructure.Data.Configuration
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
            //deleted
            status = new JobStatus()
            {
                Id = 4,
                Name = "Deleted"
            };

            jobStatus.Add(status);
            //completed
            status = new JobStatus()
            {
                Id = 5,
                Name = "Completed"
            };

            jobStatus.Add(status);
            return jobStatus;
        }
    }
}

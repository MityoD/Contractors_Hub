namespace ContractorsHub.Core.Models.Admin
{
    public class StatisticViewModel
    {
        public int AllJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int PendingJobs { get; set; }
        public int DeclinedJobs { get; set; }
        public int CompletedJobs { get; set; }
    }
}

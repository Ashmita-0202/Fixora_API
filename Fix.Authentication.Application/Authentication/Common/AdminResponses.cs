namespace Fix.Authentication.Application.Authentication.Common
{
    public class DashboardStatsResponse
    {
        public int TotalCustomers { get; set; }
        public int TotalTechnicians { get; set; }
        public int PendingTechnicianApplications { get; set; }

        public int TotalBookings { get; set; }
        public int PendingBookings { get; set; }
        public int ScheduledBookings { get; set; }
        public int InProgressBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int CancelledBookings { get; set; }

        public decimal TotalRevenue { get; set; }          // sum of Amount for Completed bookings
        public double CompletionRatePercent { get; set; }  // Completed / (Total - Cancelled)
    }

    public class CustomerSummaryResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int TotalBookings { get; set; }
        public DateTime? JoinedAt { get; set; }
    }
}

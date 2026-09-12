namespace Fix.Authentication.Application.Authentication.Commands.CreateBooking
{
    public record CreateBookingRequest
    (
        string ServiceCategoryId,
        DateTime ScheduledDate,
        string ScheduledTime,
        string Address,
        string? Notes
    );
}

namespace Fix.Authentication.Application.Authentication.Commands.CreateBooking
{
    public static class CreateBookingRequestMapper
    {
        public static CreateBookingCommand ToCommand(this CreateBookingRequest request, string customerId)
        {
            return new CreateBookingCommand
            (
                customerId,
                request.ServiceCategoryId,
                request.ScheduledDate,
                request.ScheduledTime,
                request.Address,
                request.Notes
            );
        }
    }
}

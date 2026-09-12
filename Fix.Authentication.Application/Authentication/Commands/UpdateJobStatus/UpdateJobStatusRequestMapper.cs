namespace Fix.Authentication.Application.Authentication.Commands.UpdateJobStatus
{
    public static class UpdateJobStatusRequestMapper
    {
        public static UpdateJobStatusCommand ToCommand(this UpdateJobStatusRequest request, string bookingId, string technicianId)
        {
            return new UpdateJobStatusCommand(bookingId, technicianId, request.NewStatus);
        }
    }
}

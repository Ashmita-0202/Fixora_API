namespace Fix.Authentication.Application.Authentication.Commands.UpdateJobStatus
{
    // NewStatus should be "InProgress" or "Completed"
    public record UpdateJobStatusRequest(string NewStatus);
}

using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateJobStatus
{
    public record UpdateJobStatusCommand(string BookingId, string TechnicianId, string NewStatus) : IRequest<Result<BookingResponse>>;
}

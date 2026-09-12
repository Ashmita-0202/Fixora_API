using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetAllTechnicians
{
    public record GetAllTechniciansQuery(bool? ActiveOnly) : IRequest<Result<List<TechnicianResponse>>>;
}

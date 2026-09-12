using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fix.Shared.Domain.Models.Entities;

namespace Fix.Authentication.Application.Authentication.Commands.AddUser
{
    public record AddUserRequest
    (
        string FirstName,
        string LastName,
        string Address,
        string Phone,
        string Email,
        string Password,
        UserRole UserRole,

        // Only required/used when UserRole == Technician
        string? Specialty,
        string? City,
        int? ExperienceYears,
        string? Certification
    );
}

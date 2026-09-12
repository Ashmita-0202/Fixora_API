using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fix.Authentication.Application.Authentication.Commands.AddUser
{
    public static class AddUserRequestMapper
    {

        public static AddUserCommand ToCommand(this AddUserRequest request)
        {
            return new AddUserCommand
                (
                    request.FirstName,
                    request.LastName,
                    request.Address,
                    request.Phone,
                    request.Email,
                    request.Password,
                    request.UserRole,
                    request.Specialty,
                    request.City,
                    request.ExperienceYears,
                    request.Certification
                );
        }
    }
}

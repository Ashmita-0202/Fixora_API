using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using FluentValidation;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<AddUserCommandResponse>>
    {
        public readonly IUserServices _userService;
        public readonly IMapper _mapper;

        public AddUserCommandHandler(IUserServices userServices, IMapper mapper)
        {
            _userService = userServices;
            _mapper = mapper;
        }

        public async Task<Result<AddUserCommandResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var addUser = await _userService.Add(request);

                if (addUser is null)
                {
                    return Result<AddUserCommandResponse>.Failure(
                        "Unable to add user.");
                }

                if (!addUser.IsSuccess)
                {
                    if (addUser.Errors != null && addUser.Errors.Any())
                    {
                        var errors = string.Join(", ", addUser.Errors);

                        return Result<AddUserCommandResponse>.Failure(errors);
                    }

                    return Result<AddUserCommandResponse>.Failure(
                        "Unable to add user.");
                }

                return addUser;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An error occurred while adding Customer",
                    ex);
            }
        }
    } }

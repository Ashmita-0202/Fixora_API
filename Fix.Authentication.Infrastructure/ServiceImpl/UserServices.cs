using System.Security.Cryptography;
using System.Transactions;
using AutoMapper;
using Fix.Authentication.Application.Authentication.Commands.AddUser;
using Fix.Authentication.Application.Authentication.Commands.Login;
using Fix.Authentication.Application.Authentication.Commands.ResetPassword;
using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public UserServices(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<Result<AddUserCommandResponse>> Add(AddUserCommand request)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var emailExists = await _unitOfWork.BaseRepository<User>()
                        .AnyAsync(u => u.Email == request.Email);

                    if (emailExists)
                    {
                        return Result<AddUserCommandResponse>.Failure("Conflict: An account with this email already exists.");
                    }

                    var userData = new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Address = request.Address,
                        Phone = request.Phone,
                        Email = request.Email,
                        PasswordHash = _passwordHasher.Hash(request.Password),
                        UserRole = request.UserRole,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = null
                    };

                    await _unitOfWork.BaseRepository<User>().AddAsync(userData);

                    if (request.UserRole == UserRole.Technician)
                    {
                        var profile = new TechnicianProfile(
                            Guid.NewGuid().ToString(),
                            userData.Id.ToString(),
                            request.Specialty!,
                            request.City!,
                            request.ExperienceYears!.Value,
                            request.Certification
                        );

                        await _unitOfWork.BaseRepository<TechnicianProfile>().AddAsync(profile);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddUserCommandResponse>(userData);
                    return Result<AddUserCommandResponse>.Success(resultDTOs);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding user ", ex);
                }
            }
        }

        public async Task<Result<LoginCommandResponse>> Login(LoginCommand request)
        {
            var user = await _unitOfWork.BaseRepository<User>()
                .GetSingleAsync(u => u.Email == request.Email);

            if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Result<LoginCommandResponse>.Failure("Unauthorized: Invalid email or password.");
            }

            var token = _tokenService.GenerateToken(user);

            var response = new LoginCommandResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserRole = user.UserRole,
                Token = token
            };

            if (user.UserRole == UserRole.Technician)
            {
                var profile = await _unitOfWork.BaseRepository<TechnicianProfile>()
                    .GetSingleAsync(p => p.UserId == user.Id.ToString());

                if (profile is not null)
                {
                    response.TechnicianApprovalStatus = profile.ApprovalStatus.ToString();
                    response.TechnicianIsActive = profile.IsActive;
                }
            }

            return Result<LoginCommandResponse>.Success(response);
        }

        public async Task GenerateResetToken(string email)
        {
            var user = await _unitOfWork.BaseRepository<User>()
                .GetSingleAsync(u => u.Email == email);

            // Deliberately does nothing if the user doesn't exist, so the caller
            // (ForgotPasswordCommandHandler) can't use this to discover valid emails.
            if (user is null)
            {
                return;
            }

            var rawToken = GenerateSecureToken();
            user.PasswordResetToken = HashToken(rawToken);
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BaseRepository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            await _emailService.SendPasswordResetEmailAsync(user.Email, rawToken);
        }

        public async Task<Result<ResetPasswordCommandResponse>> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _unitOfWork.BaseRepository<User>()
                .GetSingleAsync(u => u.Email == email);

            if (user is null
                || string.IsNullOrEmpty(user.PasswordResetToken)
                || user.PasswordResetTokenExpiry is null
                || user.PasswordResetTokenExpiry < DateTime.UtcNow
                || user.PasswordResetToken != HashToken(token))
            {
                return Result<ResetPasswordCommandResponse>.Failure("Invalid or expired reset token.");
            }

            user.PasswordHash = _passwordHasher.Hash(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BaseRepository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result<ResetPasswordCommandResponse>.Success(
                new ResetPasswordCommandResponse { Message = "Password has been reset successfully." });
        }

        public async Task<Result<MyProfileResponse>> GetProfile(string userId)
        {
            var user = await _unitOfWork.BaseRepository<User>()
                .GetSingleAsync(u => u.Id == Guid.Parse(userId));

            if (user is null)
            {
                return Result<MyProfileResponse>.Failure("NotFound: User not found.");
            }

            return Result<MyProfileResponse>.Success(ToProfileResponse(user));
        }

        public async Task<Result<MyProfileResponse>> UpdateProfile(string userId, string firstName, string lastName, string phone, string address)
        {
            var user = await _unitOfWork.BaseRepository<User>()
                .GetSingleAsync(u => u.Id == Guid.Parse(userId));

            if (user is null)
            {
                return Result<MyProfileResponse>.Failure("NotFound: User not found.");
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Phone = phone;
            user.Address = address;
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BaseRepository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result<MyProfileResponse>.Success(ToProfileResponse(user));
        }

        public async Task<Result<MyProfileResponse>> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _unitOfWork.BaseRepository<User>()
                .GetSingleAsync(u => u.Id == Guid.Parse(userId));

            if (user is null || !_passwordHasher.Verify(currentPassword, user.PasswordHash))
            {
                return Result<MyProfileResponse>.Failure("Unauthorized: Current password is incorrect.");
            }

            user.PasswordHash = _passwordHasher.Hash(newPassword);
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BaseRepository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result<MyProfileResponse>.Success(ToProfileResponse(user));
        }

        private static MyProfileResponse ToProfileResponse(User user)
        {
            return new MyProfileResponse
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                UserRole = user.UserRole.ToString(),
                CreatedAt = user.CreatedAt
            };
        }

        private static string GenerateSecureToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }

        private static string HashToken(string token)
        {
            var bytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }
    }
}

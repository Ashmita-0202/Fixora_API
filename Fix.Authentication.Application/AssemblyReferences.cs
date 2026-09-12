using System.Reflection;
using Fix.Authentication.Application.Authentication.Commands.AcceptJob;
using Fix.Authentication.Application.Authentication.Commands.AddServiceCategories;
using Fix.Authentication.Application.Authentication.Commands.AddUser;
using Fix.Authentication.Application.Authentication.Commands.ApproveTechnician;
using Fix.Authentication.Application.Authentication.Commands.CancelBooking;
using Fix.Authentication.Application.Authentication.Commands.ChangePassword;
using Fix.Authentication.Application.Authentication.Commands.CreateBooking;
using Fix.Authentication.Application.Authentication.Commands.CreateReview;
using Fix.Authentication.Application.Authentication.Commands.DeleteServiceCategory;
using Fix.Authentication.Application.Authentication.Commands.ForgotPassword;
using Fix.Authentication.Application.Authentication.Commands.Login;
using Fix.Authentication.Application.Authentication.Commands.MarkNotificationAsRead;
using Fix.Authentication.Application.Authentication.Commands.RejectTechnician;
using Fix.Authentication.Application.Authentication.Commands.ResetPassword;
using Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus;
using Fix.Authentication.Application.Authentication.Commands.ToggleTechnicianActiveStatus;
using Fix.Authentication.Application.Authentication.Commands.UpdateJobStatus;
using Fix.Authentication.Application.Authentication.Commands.UpdateProfile;
using Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory;
using Fix.Authentication.Application.Authentication.Commands.UpdateTechnicianProfile;
using Fix.Authentication.Application.Authentication.Queries.GetBookingById;
using Fix.Authentication.Application.Authentication.Queries.GetServiceCategoryById;
using Fix.Authentication.Application.Authentication.Queries.GetTechnicianReviews;
using Fix.Authentication.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Authentication.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddAuthenticationApplication(
           this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddScoped<IValidator<AddUserCommand>, AddUserCommandValidator>();
            services.AddScoped<IValidator<AddServiceCategoriesCommand>, AddServiceCategoriesCommandValidator>();
            services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
            services.AddScoped<IValidator<ForgotPasswordCommand>, ForgotPasswordCommandValidator>();
            services.AddScoped<IValidator<ResetPasswordCommand>, ResetPasswordCommandValidator>();
            services.AddScoped<IValidator<UpdateServiceCategoryCommand>, UpdateServiceCategoryCommandValidator>();
            services.AddScoped<IValidator<ToggleServiceCategoryStatusCommand>, ToggleServiceCategoryStatusCommandValidator>();
            services.AddScoped<IValidator<DeleteServiceCategoryCommand>, DeleteServiceCategoryCommandValidator>();
            services.AddScoped<IValidator<GetServiceCategoryByIdQuery>, GetServiceCategoryByIdQueryValidator>();
            services.AddScoped<IValidator<CreateBookingCommand>, CreateBookingCommandValidator>();
            services.AddScoped<IValidator<AcceptJobCommand>, AcceptJobCommandValidator>();
            services.AddScoped<IValidator<UpdateJobStatusCommand>, UpdateJobStatusCommandValidator>();
            services.AddScoped<IValidator<CancelBookingCommand>, CancelBookingCommandValidator>();
            services.AddScoped<IValidator<GetBookingByIdQuery>, GetBookingByIdQueryValidator>();
            services.AddScoped<IValidator<ApproveTechnicianCommand>, ApproveTechnicianCommandValidator>();
            services.AddScoped<IValidator<RejectTechnicianCommand>, RejectTechnicianCommandValidator>();
            services.AddScoped<IValidator<ToggleTechnicianActiveStatusCommand>, ToggleTechnicianActiveStatusCommandValidator>();
            services.AddScoped<IValidator<CreateReviewCommand>, CreateReviewCommandValidator>();
            services.AddScoped<IValidator<GetTechnicianReviewsQuery>, GetTechnicianReviewsQueryValidator>();
            services.AddScoped<IValidator<UpdateProfileCommand>, UpdateProfileCommandValidator>();
            services.AddScoped<IValidator<ChangePasswordCommand>, ChangePasswordCommandValidator>();
            services.AddScoped<IValidator<UpdateTechnicianProfileCommand>, UpdateTechnicianProfileCommandValidator>();
            services.AddScoped<IValidator<MarkNotificationAsReadCommand>, MarkNotificationAsReadCommandValidator>();

            return services;
        }

    }
}

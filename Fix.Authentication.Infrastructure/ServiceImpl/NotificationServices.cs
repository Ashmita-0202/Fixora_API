using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    public class NotificationServices : INotificationServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(string userId, string type, string title, string body)
        {
            var notification = new Notification(Guid.NewGuid().ToString(), userId, type, title, body);
            await _unitOfWork.BaseRepository<Notification>().AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Result<List<NotificationResponse>>> GetMine(string userId, bool? unreadOnly)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<Notification>().GetAllAsync();
                query = query.Where(n => n.UserId == userId);

                if (unreadOnly == true)
                {
                    query = query.Where(n => !n.IsRead);
                }

                var notifications = await query.AsNoTracking().OrderByDescending(n => n.CreatedAt).ToListAsync();

                var response = notifications.Select(n => new NotificationResponse
                {
                    Id = n.Id,
                    Type = n.Type,
                    Title = n.Title,
                    Body = n.Body,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                }).ToList();

                return Result<List<NotificationResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching notifications", ex);
            }
        }

        public async Task<Result<NotificationResponse>> MarkAsRead(string notificationId, string userId)
        {
            try
            {
                var notification = await _unitOfWork.BaseRepository<Notification>()
                    .GetSingleAsync(n => n.Id == notificationId);

                if (notification is null || notification.UserId != userId)
                {
                    return Result<NotificationResponse>.Failure("NotFound: Notification not found.");
                }

                notification.IsRead = true;
                _unitOfWork.BaseRepository<Notification>().Update(notification);
                await _unitOfWork.SaveChangesAsync();

                return Result<NotificationResponse>.Success(new NotificationResponse
                {
                    Id = notification.Id,
                    Type = notification.Type,
                    Title = notification.Title,
                    Body = notification.Body,
                    IsRead = notification.IsRead,
                    CreatedAt = notification.CreatedAt
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while marking notification as read", ex);
            }
        }

        public async Task<Result<string>> MarkAllAsRead(string userId)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<Notification>().GetAllAsync();
                var unread = await query.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();

                foreach (var notification in unread)
                {
                    notification.IsRead = true;
                    _unitOfWork.BaseRepository<Notification>().Update(notification);
                }

                await _unitOfWork.SaveChangesAsync();

                return Result<string>.Success($"{unread.Count} notification(s) marked as read.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while marking all notifications as read", ex);
            }
        }
    }
}

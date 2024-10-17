using AppSquareTask.Application.IServices;
using AppSquareTask.Infrastracture.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IHubContext<Infrastracture.Hubs.NotificationHub> _hubContext;

		public NotificationService(IHubContext<NotificationHub> hubContext)
		{
			_hubContext = hubContext;
		}
		public async Task NotifyAdminAsync(string message)
		{
			await _hubContext.Clients.Group("Admins").SendAsync("ReceiveNotification", message);
		}
	}
}

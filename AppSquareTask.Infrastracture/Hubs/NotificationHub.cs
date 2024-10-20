using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Infrastracture.Hubs
{
	public class NotificationHub : Hub
	{
		public override Task OnConnectedAsync()
		{
			return base.OnConnectedAsync();
		}

		public async Task JoinGroup(string groupName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		}
	}
}

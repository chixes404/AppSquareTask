using AppSquareTask.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

public class UserService // Or whatever service you're implementing
{
	private readonly UserManager<ApplicationUser> _userManager;

	public UserService(UserManager<ApplicationUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
	{
		var user = await _userManager.FindByIdAsync(userId.ToString());
		if (user == null)
		{
			throw new KeyNotFoundException("User not found.");
		}
		return user; // Return the found user
	}
}

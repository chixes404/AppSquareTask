﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.IServices
{
	public interface IEmailService
	{
		Task SendEmailAsync(string toEmail, string subject, string body);
	}
}

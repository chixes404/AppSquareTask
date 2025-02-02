﻿using AppSquareTask.Application.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task SendEmailAsync(string toEmail, string subject, string body)
		{
			var smtpSettings = _configuration.GetSection("EmailSettings");

			var smtpClient = new SmtpClient(smtpSettings["Host"])
			{
				Port = int.Parse(smtpSettings["Port"]!),
				Credentials = new NetworkCredential(smtpSettings["UserName"], smtpSettings["Password"]),
				EnableSsl = true
			};
			var mailMessage = new MailMessage
			{
				From = new MailAddress(smtpSettings["UserName"]!),
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			};
			mailMessage.To.Add(toEmail);

			try
			{
				await smtpClient.SendMailAsync(mailMessage);
			}
			catch (Exception ex)
			{
				throw new Exception($"Exception occurred while sending email to {toEmail}. Subject: {subject}", ex);
			}
		}
	}
}

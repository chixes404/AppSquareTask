using AppSquareTask.Core.MediatrHandelr.Auth.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Auth.AdminLogin
{
		public class AdminLoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public AdminLoginCommandValidator()
		{
			RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
		}
	}
}

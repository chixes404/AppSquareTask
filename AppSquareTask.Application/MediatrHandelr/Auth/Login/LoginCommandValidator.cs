﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Auth.Login
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
		}
	}
}

using AppSquareTask.Core.MediatrHandelr.Auth.OwnerRegister;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Auth.AdminRegister
{
public class AdminRegisterCommandValidation : AbstractValidator<AdminRegisterCommand>
	{
		public AdminRegisterCommandValidation()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage(" name is required.");
			RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
			RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
		}
	}
}

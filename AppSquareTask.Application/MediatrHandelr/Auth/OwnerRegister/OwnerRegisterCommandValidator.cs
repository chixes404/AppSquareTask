using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AppSquareTask.Application.MediatrHandelr.Auth.OwnerRegister
{
	public class RegisterCommandValidator : AbstractValidator<OwnerRegisterCommand>
	{
		public RegisterCommandValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage(" name is required.");
			RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
			RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
		}
	}
}

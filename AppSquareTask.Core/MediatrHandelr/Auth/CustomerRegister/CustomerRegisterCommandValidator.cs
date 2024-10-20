using AppSquareTask.Application.Dtos;
using AppSquareTask.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AppSquareTask.Core.MediatrHandelr.Auth.CustomerRegister
{
	public class CustomerRegisterCommandValidator : AbstractValidator<CustomerRegisterCommand>
	{
		public CustomerRegisterCommandValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().MaximumLength(15).WithMessage(" name is required.");
			RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
			RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
		}
	}
}

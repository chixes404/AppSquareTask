using AppSquareTask.Application.MediatrHandelr.Boat.Commands.CreateBoat;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Booking.Commands.BookBoat
{
	public class CreateBoatCommandValidator : AbstractValidator<BookBoatCommand>
	{
		public CreateBoatCommandValidator()
		{


			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("User ID is required.");



			RuleFor(x => x.BoatId)
				.NotEmpty().WithMessage("Boat ID is required.");



			RuleFor(x => x.Purpose)
				.NotEmpty().WithMessage("Purpose is required.");


		}
	}
}


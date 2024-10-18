using AppSquareTask.Application.MediatrHandelr.Boat.Commands.CreateBoat;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Booking.Commands.BookTrip
{
	public class CreateTripCommandValidator : AbstractValidator<BookTripCommand>
	{
		public CreateTripCommandValidator()
		{
			

			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("User ID is required.");



			RuleFor(x => x.TripId)
				.NotEmpty().WithMessage("Trip ID is required.");



			RuleFor(x => x.NumberOfParticipants)
				.NotEmpty().WithMessage("Number of Partcipent is required.");

		}
	}
}


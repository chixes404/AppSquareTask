using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Trip.Commands.CreateTrip
{
	public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
	{
		public CreateTripCommandValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Trip title is required.")
				.MaximumLength(200).WithMessage("Trip title must not exceed 200 characters.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Trip description is required.")
				.MaximumLength(2000).WithMessage("Trip description must not exceed 2000 characters.");

			
			RuleFor(x => x.PricePerPerson)
				.GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

			RuleFor(x => x.BoatId)
				.NotEmpty().WithMessage("Boat ID is required.");

			RuleFor(x => x.OwnerId)
				.NotEmpty().WithMessage("Owner ID is required.");

		   RuleFor(x => x.MaxCancellationPeriod)
			.NotEmpty().WithMessage("Max cancellation period is required.")
			.Must(maxCancellationPeriod => maxCancellationPeriod > DateTime.UtcNow)
			.WithMessage("Max cancellation period must be in the future.");

			RuleFor(x => x.Capacity)
				.NotEmpty().WithMessage("Capacity is required.");
			
		}
	}
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.MediatrHandelr.Trip.Commands.UpdateTrip
{
	public class UpdateTripCommandValidator : AbstractValidator<UpdateTripCommand>
	{
		public UpdateTripCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage("Trip ID is required.");

			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Course title is required.")
				.MaximumLength(200).WithMessage("Course title must not exceed 200 characters.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Course description is required.")
				.MaximumLength(2000).WithMessage("Course description must not exceed 2000 characters.");


			RuleFor(x => x.PricePerPerson)
				.GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

	

			RuleFor(x => x.MaxCancellationPeriod)
			 .NotEmpty().WithMessage("Max cancellation period is required.")
			 .Must(maxCancellationPeriod => maxCancellationPeriod > DateTime.UtcNow)
			 .WithMessage("Max cancellation period must be in the future.");

			RuleFor(x => x.Capacity)
				.NotEmpty().WithMessage("Capacity is required.");



		}
	}
}

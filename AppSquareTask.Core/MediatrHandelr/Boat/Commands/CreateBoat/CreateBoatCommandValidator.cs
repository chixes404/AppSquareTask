using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Commands.CreateBoat
{
	public class CreateBoatCommandValidator : AbstractValidator<CreateBoatCommand>
	{
		public CreateBoatCommandValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Boat title is required.")
				.MaximumLength(200).WithMessage("Boat title must not exceed 200 characters.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Boat description is required.")
				.MaximumLength(2000).WithMessage("Boat description must not exceed 2000 characters.");


			RuleFor(x => x.Price)
				.GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

		
			RuleFor(x => x.OwnerId)
				.NotEmpty().WithMessage("Owner ID is required.");

			RuleFor(x => x.Capacity)
				.NotEmpty().WithMessage("Capacity is required.");

		}
	}
}

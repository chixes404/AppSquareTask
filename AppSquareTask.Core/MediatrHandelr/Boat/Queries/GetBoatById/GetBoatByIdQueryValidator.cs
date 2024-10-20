using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetBoatById
{
	public class GetBoatByIdQueryValidator : AbstractValidator<GetBoatByIdQuery>
	{
		public GetBoatByIdQueryValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Boat ID is required");
		}
	}
}

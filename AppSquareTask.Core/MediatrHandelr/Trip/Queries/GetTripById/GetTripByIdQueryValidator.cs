using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Trip.Queries.GetTripById
{
	public class GetTripByIdQueryValidator : AbstractValidator<GetTripByIdQuery>
	{
		public GetTripByIdQueryValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("TRip ID is required");
		}
	}
}

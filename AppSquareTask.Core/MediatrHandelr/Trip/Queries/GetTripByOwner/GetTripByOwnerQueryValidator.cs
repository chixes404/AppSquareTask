using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Trip.Queries.GetTripByOwner
{
	
	public class GetTripByOwnerQueryValidator : AbstractValidator<GetTripByOwnerQuery>
	{
		public GetTripByOwnerQueryValidator()
		{
			RuleFor(x => x.OwnerId).NotEmpty().WithMessage("Owner ID is required");
		}
	}
}

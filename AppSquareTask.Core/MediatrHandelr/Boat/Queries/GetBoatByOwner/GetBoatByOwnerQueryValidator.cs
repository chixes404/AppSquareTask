using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetBoatByOwner
{

	public class GetBoatByOwnerQueryValidator : AbstractValidator<GetBoatByOwnerQuery>
	{
		public GetBoatByOwnerQueryValidator()
		{
			RuleFor(x => x.OwnerId).NotEmpty().WithMessage("Owner ID is required");
		}
	}
}

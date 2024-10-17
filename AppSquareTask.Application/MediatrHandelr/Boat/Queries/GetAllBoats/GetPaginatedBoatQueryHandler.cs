using AutoMapper;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Boat; // Ensure this namespace is included
using AppSquareTask.Application.Responses; // Ensure this namespace is included
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.MediatrHandelr.Boat.Queries.GetAllBoats;

public class GetPaginatedBoatsQueryHandler : IRequestHandler<GetPaginatedBoatsQuery, PagedList<BoatDto>>
{
	private readonly IBoatService _boatService;
	private readonly IMapper _mapper;

	public GetPaginatedBoatsQueryHandler(IBoatService boatService, IMapper mapper)
	{
		_boatService = boatService;
		_mapper = mapper;
	}

	public async Task<PagedList<BoatDto>> Handle(GetPaginatedBoatsQuery request, CancellationToken cancellationToken)
	{
		var pagedBoats = await _boatService.GetAllBoatsPaginatedAsync(request.PageNumber, request.PageSize);

		// Map the PagedList<Boat> to PagedList<BoatDto>
		var mappedBoats = new PagedList<BoatDto>(
			pagedBoats.Items.Select(boat => _mapper.Map<BoatDto>(boat)), // Map each Boat to BoatDto
			pagedBoats.PageNumber,
			pagedBoats.PageSize,
			pagedBoats.TotalCount
		);

		return mappedBoats;
	}
}

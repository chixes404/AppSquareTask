using AutoMapper;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.MediatrHandelr.Trip; // Ensure this namespace is included
using AppSquareTask.Application.Responses; // Ensure this namespace is included
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Application.MediatrHandelr.Trip.Queries.GetAllTrips;

public class GetPaginatedTripsQueryHandler : IRequestHandler<GetPaginatedTripsQuery, PagedList<TripDto>>
{
	private readonly ITripService _tripService;
	private readonly IMapper _mapper;

	public GetPaginatedTripsQueryHandler(ITripService tripService, IMapper mapper)
	{
		_tripService = tripService;
		_mapper = mapper;
	}

	public async Task<PagedList<TripDto>> Handle(GetPaginatedTripsQuery request, CancellationToken cancellationToken)
	{
		var pagedTrips = await _tripService.GetAllTripsPaginatedAsync(request.PageNumber, request.PageSize);

		// Map the PagedList<Trip> to PagedList<TripDto>
		var mappedTrips = new PagedList<TripDto>(
			pagedTrips.Items.Select(trip => _mapper.Map<TripDto>(trip)), // Map each Trip to TripDto
			pagedTrips.PageNumber,
			pagedTrips.PageSize,
			pagedTrips.TotalCount
		);

		return mappedTrips;
	}
}

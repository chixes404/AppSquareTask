using AutoMapper;
using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Trip; // Ensure this namespace is included
using AppSquareTask.Core.Responses; // Ensure this namespace is included
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Core.MediatrHandelr.Trip.Queries.GetAllTrips;
using AppSquareTask.Data.Models;
using AppSquareTask.Infrastracture.IRepositories;
using AppSquareTask.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;

public class GetPaginatedTripsQueryHandler : IRequestHandler<GetPaginatedTripsQuery, PagedList<TripDto>>
{
	private readonly ITripService _tripService;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public GetPaginatedTripsQueryHandler(ITripService tripService, IMapper mapper , IUnitOfWork unitofwork )
	{
		_tripService = tripService;
		_mapper = mapper;
		_unitOfWork = unitofwork;
	}

	public async Task<PagedList<TripDto>> Handle(GetPaginatedTripsQuery request, CancellationToken cancellationToken)
	{
		var approvedTripsQuery = _unitOfWork.TripRepository.Query()
			.Where(trip => trip.Status == Status.Approved);

		var totalCount = await approvedTripsQuery.CountAsync(cancellationToken);

		var paginatedTrips = await approvedTripsQuery
			.Skip((request.PageNumber - 1) * request.PageSize)
			.Take(request.PageSize)
			.ToListAsync(cancellationToken);

		var mappedTrips = paginatedTrips.Select(trip => _mapper.Map<TripDto>(trip)).ToList();

		return new PagedList<TripDto>(mappedTrips, request.PageNumber, request.PageSize, totalCount);
	}

}

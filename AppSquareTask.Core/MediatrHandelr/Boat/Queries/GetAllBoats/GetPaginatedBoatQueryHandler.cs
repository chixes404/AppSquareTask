using AutoMapper;
using AppSquareTask.Application.IServices;
using AppSquareTask.Core.MediatrHandelr.Boat; // Ensure this namespace is included
using AppSquareTask.Core.Responses; // Ensure this namespace is included
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AppSquareTask.Core.MediatrHandelr.Boat.Queries.GetAllBoats;
using AppSquareTask.Data.Models;
using AppSquareTask.Infrastracture.IRepositories;
using Microsoft.EntityFrameworkCore;

public class GetPaginatedBoatsQueryHandler : IRequestHandler<GetPaginatedBoatsQuery, PagedList<BoatDto>>
{
	private readonly IBoatService _boatService;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public GetPaginatedBoatsQueryHandler(IBoatService boatService, IMapper mapper , IUnitOfWork unitOfWork)
	{
		_boatService = boatService;
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	public async Task<PagedList<BoatDto>> Handle(GetPaginatedBoatsQuery request, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.BoatRepository.Query()
			.Where(boat => boat.Status == Status.Approved);

		var totalCount = await query.CountAsync(cancellationToken);

		var paginatedBoats = await query
			.Skip((request.PageNumber - 1) * request.PageSize)
			.Take(request.PageSize)
			.ToListAsync(cancellationToken);

		var mappedBoats = paginatedBoats
			.Select(boat => _mapper.Map<BoatDto>(boat))
			.ToList();

		return new PagedList<BoatDto>(mappedBoats, request.PageNumber, request.PageSize, totalCount);
	}

}

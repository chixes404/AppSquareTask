using AppSquareTask.Application.IServices;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using System;
using System.Threading.Tasks;

public class BookingService : IBookingService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IWalletService _walletService;
	private readonly UserService _userService;

	public BookingService(IUnitOfWork unitOfWork, IWalletService walletService, UserService userService)
	{
		_unitOfWork = unitOfWork;
		_walletService = walletService;
		_userService = userService;
	}

	public async Task<TripBooking> BookTripAsync(int tripId, Guid userId, int numberOfParticipants)
	{
		var trip = await _unitOfWork.TripRepository.GetById(tripId);
		if (trip == null || trip.Status != Status.Approved)
		{
			throw new KeyNotFoundException("Trip not found or not approved.");
		}

		var user = await _userService.GetUserByIdAsync(userId);
		if (user == null)
		{
			throw new KeyNotFoundException("User not found.");
		}

		var totalPrice = trip.PricePerPerson * numberOfParticipants;

		var isPaymentSuccessful = await _walletService.Pay(userId, totalPrice);
		if (!isPaymentSuccessful)
		{
			throw new InvalidOperationException("Payment failed.");
		}

		var customer = await _unitOfWork.Repository<Customer>()
						 .FindAsync(c => c.UserId == userId);
		var existingCustomer = customer.FirstOrDefault(); // Get the first matching customer

		if (existingCustomer == null)
		{
			throw new KeyNotFoundException("Customer not found.");
		}

		var tripBooking = new TripBooking
		{
			TripId = tripId,
			CustomerId = existingCustomer.Id,
			NumberOfParticipants = numberOfParticipants,
			TotalPrice = totalPrice,
			BookingDate = DateTime.UtcNow,
			IsPaid = true
		};

		await _unitOfWork.TripBookingRepository.CreateAsync(tripBooking);
		await _unitOfWork.SaveAsync();

		return tripBooking;
	}

	public async Task<BoatBooking> BookBoatAsync(int boatId, Guid userId ,int capacaity)
	{
		var boat = await _unitOfWork.BoatRepository.GetById(boatId);
		if (boat == null)
		{
			throw new KeyNotFoundException("Boat not found.");
		}

		var user = await _userService.GetUserByIdAsync(userId);
		if (user == null)
		{
			throw new KeyNotFoundException("User not found.");
		}

		var totalPrice = boat.PricePerPerson;

		var isPaymentSuccessful = await _walletService.Pay(userId, totalPrice);
		if (!isPaymentSuccessful)
		{
			throw new InvalidOperationException("Payment failed.");
		}

		var customer = await _unitOfWork.Repository<Customer>()
						 .FindAsync(c => c.UserId == userId);
		var existingCustomer = customer.FirstOrDefault(); // Get the first matching customer

		if (existingCustomer == null)
		{
			throw new KeyNotFoundException("Customer not found.");
		}
		var boatBooking = new BoatBooking
		{
			BoatId = boatId,
			CustomerId = existingCustomer.Id,
			TotalPrice = totalPrice,
			BookingDate = DateTime.UtcNow,
			IsPaid = true
		};

		await _unitOfWork.BoatBookingRepository.CreateAsync(boatBooking);
		await _unitOfWork.SaveAsync();

		return boatBooking;
	}
}

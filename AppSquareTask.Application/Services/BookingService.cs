using AppSquareTask.Application.Dtos;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Responses;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using AppSquareTask.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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







public async Task<BoatBooking> GetBoatBookingByIdAsync(int bookingId)
{
		// Retrieve the booking along with related Boat and Customer entities
		var booking = await _unitOfWork.Repository<BoatBooking>()
			.FindAsync(b => b.Id == bookingId,
						include: q => q.Include(b => b.Boat)
										.Include(b => b.Customer));          

	if (booking == null)
	{
		throw new KeyNotFoundException("Booking not found.");
	}

	return (BoatBooking)booking; // Return the found booking with included related entities
}


public async Task<TripBooking> GetTripBookingByIdAsync(int bookingId)
	{
		var booking = await _unitOfWork.TripBookingRepository.GetById(bookingId);
		if (booking == null)
		{
			throw new KeyNotFoundException("Booking not found.");
		}

		return booking; 
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

			var customer = await _unitOfWork.Repository<Customer>()
								 .FindAsync(c => c.UserId == userId);
			var existingCustomer = customer.FirstOrDefault();

			if (existingCustomer == null)
			{
				throw new KeyNotFoundException("Customer not found.");
			}

			var totalPrice = trip.PricePerPerson * numberOfParticipants;


		// Begin transaction
		using var transaction = await _unitOfWork.BeginTransactionAsync();

		try
		{
			var isPaymentSuccessful = await _walletService.Pay(userId, totalPrice , tripId);
			if (!isPaymentSuccessful)
			{
				throw new InvalidOperationException("Payment failed.");
			}

			var owner = await _unitOfWork.OwnerRepository.GetById(trip.OwnerId);

			var ownerWallet = await _walletService.GetWalletByCustomerIdAsync(owner.UserId); 
			if (ownerWallet == null)
			{
				throw new KeyNotFoundException("Owner wallet not found.");
			}
			ownerWallet.Balance += totalPrice; 
			await _unitOfWork.WalletRepository.UpdateAsync(ownerWallet);

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

			await transaction.CommitAsync();

			return tripBooking;
		}
		catch (Exception ex)
		{
		
			await transaction.RollbackAsync();
			throw; 
		}
	}

	public async Task<BoatBooking> BookBoatAsync(int boatId, Guid userId, string purpose, DateTime date)
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

		var customer = await _unitOfWork.Repository<Customer>()
						.FindAsync(c => c.UserId == userId);
		var existingCustomer = customer.FirstOrDefault(); 

		if (existingCustomer == null)
		{
			throw new KeyNotFoundException("Customer not found.");
		}

		var totalPrice = boat.Price;

		// Begin transaction
		using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{
			var isPaymentSuccessful = await _walletService.Pay(userId, totalPrice , boatId);
			if (!isPaymentSuccessful)
			{
				throw new InvalidOperationException("Payment failed.");
			}

			var owner = await _unitOfWork.OwnerRepository.GetById(boat.OwnerId);

			var ownerWallet = await _walletService.GetWalletByCustomerIdAsync(owner.UserId);
			if (ownerWallet == null)
			{
				throw new KeyNotFoundException("Owner wallet not found.");
			}
			ownerWallet.Balance += totalPrice;
			await _unitOfWork.WalletRepository.UpdateAsync(ownerWallet);

			var boatBooking = new BoatBooking
			{
				BoatId = boatId,
				CustomerId = existingCustomer.Id,
				Price = totalPrice,
				Purpose = purpose,
				BookingDate = date,
				IsPaid = true
			};

			await _unitOfWork.BoatBookingRepository.CreateAsync(boatBooking);

			await _unitOfWork.SaveAsync();
			await transaction.CommitAsync();

			return boatBooking;
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			throw;
		}
	}



	public async Task CancelBoatBookingAsync(int bookingId, int customerId)
	{
		var boatBooking = await _unitOfWork.BoatBookingRepository.GetById(bookingId);
		if (boatBooking == null || boatBooking.CustomerId != customerId)
		{
			throw new KeyNotFoundException("Boat booking not found or does not belong to the customer.");
		}

		if (boatBooking.IsCanceled)
		{
			throw new InvalidOperationException("This booking has already been canceled.");
		}

		var customer = await _unitOfWork.CustomerRepository.GetById(customerId);
		if (customer == null)
		{
			throw new KeyNotFoundException("Customer not found.");
		}

		var owner = await _unitOfWork.OwnerRepository.GetById(boatBooking.Boat.OwnerId);
		if (owner == null)
		{
			throw new KeyNotFoundException("Owner not found.");
		}

		var ownerWallet = await _walletService.GetWalletByCustomerIdAsync(owner.UserId);
		if (ownerWallet == null)
		{
			throw new KeyNotFoundException("Owner wallet not found.");
		}

		if (boatBooking.IsPaid)
		{
			var refundSuccessful = await _walletService.RefundToWalletAsync(customer.UserId, boatBooking.Price );
			if (!refundSuccessful)
			{
				throw new InvalidOperationException("Refund failed.");
			}

			ownerWallet.Balance -= boatBooking.Price;
			await _unitOfWork.WalletRepository.UpdateAsync(ownerWallet);
		}

		boatBooking.IsCanceled = true;
		boatBooking.CancellationDate = DateTime.UtcNow;

		_unitOfWork.BoatBookingRepository.UpdateAsync(boatBooking);
		await _unitOfWork.SaveAsync();
	}




	public async Task CancelTripBookingAsync(int bookingId, int customerId)
	{
		var tripBooking = await _unitOfWork.TripBookingRepository.GetById(bookingId);
		if (tripBooking == null)
		{
			throw new KeyNotFoundException("Trip booking not found.");
		}

		var trip = await _unitOfWork.TripRepository.GetById(tripBooking.TripId);

		if (tripBooking.CustomerId != customerId)
		{
			throw new InvalidOperationException("This booking does not belong to the customer.");
		}

		if (tripBooking.IsCanceled)
		{
			throw new InvalidOperationException("This booking has already been canceled.");
		}


		if (DateTime.UtcNow > trip.MaxCancellationPeriod)
		{
			throw new InvalidOperationException("Cancellation period has passed");
		}



		if (tripBooking.IsPaid)
		{
			var customer = await _unitOfWork.CustomerRepository.GetById(customerId);
			if (customer == null)
			{
				throw new KeyNotFoundException("Customer not found.");
			}

			var refundSuccessful = await _walletService.RefundToWalletAsync(customer.UserId, tripBooking.TotalPrice);
			if (!refundSuccessful)
			{
				throw new InvalidOperationException("Refund failed.");
			}

			var owner = await _unitOfWork.OwnerRepository.GetById(trip.OwnerId);
			if (owner == null)
			{
				throw new KeyNotFoundException("Owner not found.");
			}

			var ownerWallet = await _walletService.GetWalletByCustomerIdAsync(owner.UserId);
			if (ownerWallet == null)
			{
				throw new KeyNotFoundException("Owner wallet not found.");
			}
			ownerWallet.Balance -= tripBooking.TotalPrice;
			await _unitOfWork.WalletRepository.UpdateAsync(ownerWallet);



		}



		tripBooking.IsCanceled = true;
		tripBooking.CancellationDate = DateTime.UtcNow;

		_unitOfWork.TripBookingRepository.UpdateAsync(tripBooking);
		await _unitOfWork.SaveAsync(); // Commit the changes
	}






}

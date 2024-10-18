using AppSquareTask.Application.IServices;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Core.Models;
using Microsoft.EntityFrameworkCore;

public class WalletService : IWalletService
{
	private readonly IUnitOfWork _unitOfWork;

	public WalletService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<bool> Pay(Guid userId, decimal amount)
	{
		var wallet = await GetWalletByCustomerIdAsync(userId); 
		if (wallet == null || wallet.Balance < amount)
		{
			return false; 
		}

		wallet.Balance -= amount;
		var transaction = new Transaction
		{
			WalletId = wallet.Id,
			Amount = amount,
			Type = "Wallet", 
			Created = DateTime.UtcNow 
		};

		await _unitOfWork.WalletRepository.UpdateAsync(wallet);

		await _unitOfWork.Repository<Transaction>().CreateAsync(transaction);

		await _unitOfWork.SaveAsync();

		return true; 
	}


	public async Task<bool> RefundToWalletAsync(Guid userId, decimal amount)
	{
		// Retrieve the wallet associated with the userId
		var wallet = await GetWalletByCustomerIdAsync(userId);
		if (wallet == null)
		{
			return false; // Wallet not found
		}

		// Increase the wallet balance by the refund amount
		wallet.Balance += amount;

		// Create a new transaction record for the refund
		var transaction = new Transaction
		{
			WalletId = wallet.Id, // Assuming wallet has an Id property
			Amount = amount,
			Type = "Credit", // Indicate that this is a credit transaction
			Created = DateTime.UtcNow // Use current date/time
		};

		// Update the wallet in the repository
		await _unitOfWork.WalletRepository.UpdateAsync(wallet);

		// Save the transaction to the repository
		await _unitOfWork.Repository<Transaction>().CreateAsync(transaction);

		// Commit all changes to the database
		await _unitOfWork.SaveAsync();

		return true; // Successful refund and transaction creation
	}









	public async Task<Wallet> GetWalletByCustomerIdAsync(Guid userId)
	{
		var wallets = await _unitOfWork.Repository<Wallet>()
						 .FindAsync(w => w.UserId == userId,
									 include: q => q.Include(w => w.User)); 

		var wallet = wallets.FirstOrDefault(); 

		if (wallet == null)
		{
			throw new KeyNotFoundException("Wallet not found.");
		}

		return wallet; // Return the found wallet
	}

}

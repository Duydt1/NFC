using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NFC.Data;
using NFC.Data.Common;
using NFC.Data.Entities;
using NFC.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static NFC.Data.Common.NFCUtil;

namespace Data.Repositories
{
	public interface IHistoryUploadRepository
	{
		Task<IEnumerable<HistoryUpload>> GetAllAsync();
		Task<PaginatedList<HistoryUpload>> GetAllAsync(FilterModel filterModel);
		Task<HistoryUpload> GetByIdAsync(long id);
		Task<IEnumerable<HistoryUpload>> GetAllFailedAsync();
		Task CreateAsync(HistoryUpload entity);
		Task UpdateAsync(HistoryUpload entity);
		Task BulkUpdateAsync(List<HistoryUpload> entities);
		Task DeleteAsync(long id);
	}

	public class HistoryUploadRepository : IHistoryUploadRepository
	{
		private readonly NFCDbContext _context;

		public HistoryUploadRepository(NFCDbContext context)
		{
			_context = context;
		}
		public async Task<PaginatedList<HistoryUpload>> GetAllAsync(FilterModel filterModel)
		{
			var query = _context.HistoryUploads as IQueryable<HistoryUpload>;

			query = query.Where(h =>
								(string.IsNullOrEmpty(filterModel.SearchString) || h.Title.Contains(filterModel.SearchString) || h.Message.Contains(filterModel.SearchString))
								&& h.ProductionLineId == filterModel.ProductionLineId
								&& h.CreatedOn >= filterModel.FromDate
								&& h.CreatedOn <= filterModel.ToDate).OrderByDescending(x => x.CreatedOn);
			var count = await query.CountAsync();
			var items = await query.Skip((filterModel.PageNumber - 1) * filterModel.PageSize).Take(filterModel.PageSize).AsNoTracking().ToListAsync();
			return new PaginatedList<HistoryUpload>(items, count, filterModel.PageNumber, filterModel.PageSize);
		}
		public async Task<IEnumerable<HistoryUpload>> GetAllAsync()
		{
			return await _context.HistoryUploads.Select(x => new HistoryUpload
			{
				Id = x.Id,
				Type = x.Type,
				CreatedOn = x.CreatedOn,
				Message = x.Message,
				CreatedById = x.CreatedById,
				Status = x.Status,
			}).OrderByDescending(x => x.CreatedOn).ToListAsync();
		}
		public async Task<IEnumerable<HistoryUpload>> GetAllFailedAsync()
		{
			return await _context.HistoryUploads.Select(x => new HistoryUpload
			{
				Id = x.Id,
				Type = x.Type,
				Title = x.Title,
				Message = x.Message,
				FileContent = x.FileContent,
				ProductionLineId = x.ProductionLineId,
				CreatedById = x.CreatedById,
				Status = x.Status,
			}).Where(x => x.Status != (int)NFCCommon.HistoryStatus.Completed && x.Status != (int)NFCCommon.HistoryStatus.Declined && x.Status != (int)NFCCommon.HistoryStatus.Processing && !string.IsNullOrEmpty(x.FileContent)).ToListAsync();
		}

		public async Task<HistoryUpload> GetByIdAsync(long id)
		{
			return await _context.HistoryUploads.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task CreateAsync(HistoryUpload entity)
		{
			await _context.HistoryUploads.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(HistoryUpload entity)
		{
			_context.HistoryUploads.Update(entity);
			await _context.SaveChangesAsync();
		}
		public async Task BulkUpdateAsync(List<HistoryUpload> entities)
		{
			int retryCount = 0;
			int maxRetries = 3;
			TimeSpan retryDelay = TimeSpan.FromMilliseconds(100);

			while (retryCount < maxRetries)
			{
				try
				{
					using var transaction = new TransactionScope(
						TransactionScopeOption.Required,
						new TransactionOptions { Timeout = TimeSpan.FromMinutes(30) },
						TransactionScopeAsyncFlowOption.Enabled);
					await _context.BulkUpdateAsync(entities);
					transaction.Complete();
					return;
				}
				catch (SqlException ex) when (ex.Number == 1205)
				{
					retryCount++;
					await Task.Delay(retryDelay);
					retryDelay = retryDelay * 2; // exponential backoff
				}
			}

		}
		public async Task DeleteAsync(long id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_context.HistoryUploads.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
	}
}

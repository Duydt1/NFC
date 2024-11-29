using Microsoft.EntityFrameworkCore;
using NFC.Data;
using NFC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public interface IProductionLineRepository
	{
		Task<IEnumerable<ProductionLine>> GetAllAsync();

		Task<List<ProductionLine>> GetAllAsync(string? userId);
		Task<ProductionLine> GetByIdAsync(int id);
		Task CreateAsync(ProductionLine entity);
		Task UpdateAsync(ProductionLine entity);
		Task DeleteAsync(int id);
	}

	public class ProductionLineRepository : IProductionLineRepository
	{
		private readonly NFCDbContext _context;
		private readonly IIdentityRepository _repository;

		public ProductionLineRepository(NFCDbContext context, IIdentityRepository repository)
		{
			_context = context;
			_repository = repository;
		}

		public async Task<IEnumerable<ProductionLine>> GetAllAsync()
		{
			return await _context.ProductionLines.OrderByDescending(x => x.CreatedOn).ToListAsync();
		}
		public async Task<List<ProductionLine>> GetAllAsync(string? userId)
		{
			var query = _context.ProductionLines.Select(x => new ProductionLine
			{
				Id = x.Id,
				Name = x.Name,
				CreatedById = x.CreatedById,
				Description = x.Description
			}).AsQueryable();

			if (string.IsNullOrEmpty(userId))
			{
				var user = await _repository.GetUserAsync(userId);
				if (user != null)
					query.Where(x => x.Id == user.ProductionLineId);

			}

			return await query.ToListAsync();
		}

		public async Task<ProductionLine> GetByIdAsync(int id)
		{
			return await _context.ProductionLines.FindAsync(id);
		}

		public async Task CreateAsync(ProductionLine entity)
		{
			_context.ProductionLines.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(ProductionLine entity)
		{
			_context.ProductionLines.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_context.ProductionLines.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
	}
}

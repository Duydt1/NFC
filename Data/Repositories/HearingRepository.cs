using Dapper;
using Data.Models;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NFC.Data;
using NFC.Data.Entities;
using NFC.Data.Models;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NFC.Data.Common.NFCUtil;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Repositories
{
	public interface IHearingRepository
	{
		Task<IEnumerable<Hearing>> GetListAsync(FilterModel filterModel);
		Task<Hearing> GetByIdAsync(long id);
		Task<List<ResultModel>> GetTotal(FilterModel filterModel);
		Task CreateAsync(Hearing entity);
		Task UpdateAsync(Hearing entity);
		Task<HearingDetailViewModel> GetHearingDetailAsync(string num);
		Task<List<Hearing>> GetListByNumAsync(string num, long id);
		Task<PaginatedList<Hearing>> GetAllAsync(FilterModel filterModel);
		Task DeleteAsync(long id);
		Task UpdateRangeAsync(IEnumerable<Hearing> entities);
		Task CreateRangeAsync(IEnumerable<Hearing> entities);
		Task<List<Hearing>> GetExistNums(List<string> existNUMs);
		Task<CheckNumHearingModel> GetHearingResultAsync(string num);
	}

	public class HearingRepository : IHearingRepository
	{
		private readonly NFCDbContext _context;

		public HearingRepository(NFCDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Hearing>> GetListAsync(FilterModel filterModel)
		{
			var query = _context.Hearings as IQueryable<Hearing>;

			query = query.Where(h =>
								(string.IsNullOrEmpty(filterModel.SearchString) || h.NUM.Contains(filterModel.SearchString) || h.Model.Contains(filterModel.SearchString))
								&& h.ProductionLineId == filterModel.ProductionLineId
								&& h.DateTime >= filterModel.FromDate
								&& h.DateTime <= filterModel.ToDate).OrderByDescending(x => x.DateTime);
			return await query.Skip((filterModel.PageNumber - 1) * filterModel.PageSize).Take(filterModel.PageSize).Distinct().AsNoTracking().ToListAsync();
		}
		public async Task<CheckNumHearingModel> GetHearingResultAsync(string num)
		{
			var query = from s in _context.Sensors
						join h in _context.Hearings on s.NUM equals h.NUM into hearingGroup
						from h in hearingGroup.DefaultIfEmpty()
						join tw in _context.KT_TW_SPLs on s.NUM equals tw.NUM into twGroup
						from tw in twGroup.DefaultIfEmpty()
						join mic in _context.KT_MIC_WF_SPLs on s.NUM equals mic.NUM into micGroup
						from mic in micGroup.DefaultIfEmpty()
						where s.NUM == num
						orderby h.DateTime descending, s.DateTime descending, tw.DateTime descending, mic.DateTime descending
						select new
						{
							Hearing = h,
							Sensor = s,
							TW = tw,
							MIC = mic
						};

			var result = await query.FirstOrDefaultAsync();
			if (result != null)
			{
				if (result.Sensor == null) return new CheckNumHearingModel { Status = 501, Message = "Miss data Sensor" };
				if (result.TW == null) return new CheckNumHearingModel { Status = 502, Message = "Miss data TW" };
				if (result.MIC == null) return new CheckNumHearingModel { Status = 503, Message = "Miss data WF" };
				if (result.Hearing == null) return new CheckNumHearingModel { Status = 504, Message = "Miss data Hearing" };

				if (result.Sensor.Result!.ToUpper() != "OK") return new CheckNumHearingModel { Status = 505, Message = "Fail Sensor" };
				if (result.TW.Result!.ToUpper() != "PASS") return new CheckNumHearingModel { Status = 506, Message = "Fail TW" };
				if (result.MIC.Result!.ToUpper() != "PASS") return new CheckNumHearingModel { Status = 507, Message = "Fail WF" };
				if (result.Hearing.Result!.ToUpper() != "PASS") return new CheckNumHearingModel { Status = 508, Message = "Fail Hearing" };

				if (result.Hearing.Result!.ToUpper() == "PASS" && result.TW.Result!.ToUpper() == "PASS" && result.MIC.Result!.ToUpper() == "PASS" && result.Sensor.Result!.ToUpper() == "OK")
					return new CheckNumHearingModel { Status = 200, Message = "OK" };
				else
					return new CheckNumHearingModel { Status = 500, Message = "NG" };
			}
			else
				return new CheckNumHearingModel { Status = 404, Message = "NOT FOUND" };

		}
		public async Task<List<ResultModel>> GetTotal(FilterModel filterModel)
		{
			var query = @"
						SELECT 
							CH, 
							COUNT(*) AS Total, 
							SUM(CASE WHEN Result = 'PASS' THEN 1 ELSE 0 END) AS TotalPass, 
							SUM(CASE WHEN Result = 'FAIL' THEN 1 ELSE 0 END) AS TotalFail, 
							0 AS TotalUpdate,
							FORMAT(CAST(SUM(CASE WHEN Result = 'FAIL' THEN 1 ELSE 0 END) AS float) / COUNT(*) * 100, 'N1') + '%' AS PercentFail
						FROM 
							Hearings
						WHERE 
							ProductionLineId = @ProductionLineId
							AND DateTime >= @FromDate
							AND DateTime <= @ToDate
						GROUP BY 
							CH
					";

			var parameters = new[]
			{
				new SqlParameter("@ProductionLineId", filterModel.ProductionLineId),
				new SqlParameter("@FromDate", filterModel.FromDate),
				new SqlParameter("@ToDate", filterModel.ToDate)
			};

			var results = await _context.Database.SqlQueryRaw<ResultModel>(query, parameters).ToListAsync();

			return results;
		}
		public async Task<PaginatedList<Hearing>> GetAllAsync(FilterModel filterModel)
		{
			// Tạo chuỗi truy vấn SQL
			var sqlQuery = @"
							WITH RankedHearings AS
							(
								SELECT h.*, 
									   ROW_NUMBER() OVER (PARTITION BY h.NUM ORDER BY h.ModifiedOn DESC) AS RowNum
								FROM Hearings h
								WHERE h.ProductionLineId = @ProductionLineId
								AND h.DateTime >= @FromDate
								AND h.DateTime <= @ToDate
								AND (@SearchString IS NULL OR h.NUM LIKE '%' + @SearchString + '%' OR h.Model LIKE '%' + @SearchString + '%')
							)
							SELECT *
							FROM RankedHearings
							WHERE RowNum = 1
							ORDER BY DateTime DESC
							OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
			//string fromDate = $"{filterModel.FromDate.Value.Year:D2}-{filterModel.FromDate.Value.Month:D2}-{filterModel.FromDate.Value.Day:D2}";
			//string toDate = $"{filterModel.ToDate.Value.Year:D2}-{filterModel.ToDate.Value.Month:D2}-{filterModel.ToDate.Value.Day:D2}";


			// Tạo các tham số
			var parameters = new[]
			{
				new SqlParameter("@ProductionLineId", filterModel.ProductionLineId),
				new SqlParameter("@SearchString", string.IsNullOrEmpty(filterModel.SearchString) ? DBNull.Value : (object)filterModel.SearchString),
				new SqlParameter("@FromDate", filterModel.FromDate),
				new SqlParameter("@ToDate", filterModel.ToDate),
				new SqlParameter("@Offset", (filterModel.PageNumber - 1) * filterModel.PageSize),
				  new SqlParameter("@PageSize", filterModel.PageSize)
			  };

			// Thực hiện truy vấn SQL
			var items = await _context.Hearings.FromSqlRaw(sqlQuery, parameters).AsNoTracking().ToListAsync();
			// Đếm số lượng kết quả thỏa mãn điều kiện
			var countQuery = @"
							WITH RankedHearings AS
					(
						SELECT h.*, 
							   ROW_NUMBER() OVER (PARTITION BY h.NUM ORDER BY h.DateTime DESC) AS RowNum
						FROM Hearings h
						WHERE h.ProductionLineId = @ProductionLineId
						AND h.DateTime >= @FromDate
						AND h.DateTime <= @ToDate
						AND (@SearchString IS NULL OR h.NUM LIKE '%' + @SearchString + '%' OR h.Model LIKE '%' + @SearchString + '%')
					)
					SELECT COUNT(*) as Total
					FROM RankedHearings
					WHERE RowNum = 1;";

			var counts = await _context.Database.SqlQueryRaw<CountModel>(countQuery, parameters).ToListAsync();
			int count = counts.FirstOrDefault()?.Total ?? 0;
			return new PaginatedList<Hearing>(items, count, filterModel.PageNumber, filterModel.PageSize);
		}
		public async Task<List<Hearing>> GetExistNums(List<string> existNUMs)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var existNUMsSet = new HashSet<string>(existNUMs);
				var parameters = new List<SqlParameter>();
				var parameterizedNums = new List<string>();

				int index = 0;
				foreach (var num in existNUMsSet)
				{
					var paramName = $"@p{index++}";
					parameterizedNums.Add(paramName);
					parameters.Add(new SqlParameter(paramName, num));
				}

				var existNUMsSetString = string.Join(",", parameterizedNums);

				var result = _context.Hearings
					.FromSqlRaw($"SELECT * FROM Hearings WHERE NUM IN ({existNUMsSetString})", parameters.ToArray())
					.ToList();
				await transaction.CommitAsync();
				return result;
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				throw;
			}
		}

		public async Task<Hearing> GetByIdAsync(long id)
		{
			return await _context.Hearings.Include(x => x.ProductionLine).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Hearing>> GetListByNumAsync(string num, long id)
		{
			return await _context.Hearings.Where(x => x.NUM.Contains(num) && x.Id != id).Include(x => x.ProductionLine).OrderByDescending(x => x.DateTime).ToListAsync();
		}

		public async Task<HearingDetailViewModel> GetHearingDetailAsync(string num)
		{
			var hearing = await _context.Hearings.Where(x => x.NUM.Contains(num)).OrderByDescending(x => x.ModifiedOn).FirstOrDefaultAsync();
			var sensor = await _context.Sensors.Where(x => x.NUM.Contains(num)).OrderByDescending(x => x.DateTime).FirstOrDefaultAsync();
			var tw = await _context.KT_TW_SPLs.Where(x => x.NUM.Contains(num)).OrderByDescending(x => x.DateTime).FirstOrDefaultAsync();
			var mic = await _context.KT_MIC_WF_SPLs.Where(x => x.NUM.Contains(num)).OrderByDescending(x => x.DateTime).FirstOrDefaultAsync();

			// Map to ViewModel

			return new HearingDetailViewModel
			{
				hearing = hearing,
				tw = tw,
				wf = mic,
				sensor = sensor,
			};

		}

		public async Task CreateAsync(Hearing entity)
		{
			_context.Hearings.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Hearing entity)
		{
			_context.Hearings.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(long id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_context.Hearings.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
		public async Task UpdateRangeAsync(IEnumerable<Hearing> entities)
		{
			bool success = false;
			int retryCount = 0;
			int maxRetries = 3;
			int delay = 5000;
			while (!success && retryCount < maxRetries)
			{
				try
				{
					var uniqueEntities = entities.GroupBy(e => e.NUM)
												 .Select(g => g.OrderByDescending(x => x.DateTime).First())
												 .ToList();
					await _context.BulkUpdateAsync(uniqueEntities);
					success = true;
				}
				catch (SqlException ex) when (ex.Number == 1205) // 1205 is the SQL error code for a deadlock
				{
					retryCount++;
					if (retryCount == maxRetries)
					{
						throw;
					}
					await Task.Delay(delay); // Sử dụng Task.Delay cho các hoạt động bất đồng bộ
					delay *= 2; // Tăng dần thời gian chờ
				}
			}
		}

		public async Task CreateRangeAsync(IEnumerable<Hearing> entities)
		{
			bool success = false;
			int retryCount = 0;
			int maxRetries = 3;
			int delay = 5000;
			while (!success && retryCount < maxRetries)
			{
				try
				{
					var uniqueEntities = entities.GroupBy(e => e.NUM)
												 .Select(g => g.OrderByDescending(x => x.DateTime).First())
												 .ToList();
					await _context.BulkInsertAsync(uniqueEntities);
					success = true;
				}
				catch (SqlException ex) when (ex.Number == 1205) // 1205 is the SQL error code for a deadlock
				{
					retryCount++;
					if (retryCount == maxRetries)
					{
						throw;
					}
					await Task.Delay(delay); // Sử dụng Task.Delay cho các hoạt động bất đồng bộ
					delay *= 2; // Tăng dần thời gian chờ
				}
			}
		}
	}
}

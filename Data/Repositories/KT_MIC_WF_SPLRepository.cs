using Data.Models;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NFC.Data;
using NFC.Data.Entities;
using NFC.Data.Models;
using static NFC.Data.Common.NFCUtil;

namespace Data.Repositories
{
    public interface IKT_MIC_WF_SPLRepository
    {
        Task<IEnumerable<KT_MIC_WF_SPL>> GetListAsync(FilterModel filterModel);
        Task<KT_MIC_WF_SPL> GetByIdAsync(long id);
        Task CreateAsync(KT_MIC_WF_SPL entity);
        Task UpdateAsync(KT_MIC_WF_SPL entity);
        Task DeleteAsync(long id);
        Task<PaginatedList<KT_MIC_WF_SPL>> GetAllAsync(FilterModel filterModel);
        Task<List<KT_MIC_WF_SPL>> GetExistNums(List<string> existNUMs);
		Task UpdateRangeAsync(IEnumerable<KT_MIC_WF_SPL> entities);
		Task CreateRangeAsync(IEnumerable<KT_MIC_WF_SPL> entities);
		Task<List<ResultModel>> GetTotal(FilterModel filterModel);
		Task<List<KT_MIC_WF_SPL>> GetListByNumAsync(string num);
	}

    public class KT_MIC_WF_SPLRepository : IKT_MIC_WF_SPLRepository
    {
        private readonly NFCDbContext _context;

        public KT_MIC_WF_SPLRepository(NFCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KT_MIC_WF_SPL>> GetListAsync(FilterModel filterModel)
        {
			var query = _context.KT_MIC_WF_SPLs as IQueryable<KT_MIC_WF_SPL>;

			query = query.Where(h =>
								(string.IsNullOrEmpty(filterModel.SearchString) || h.NUM.Contains(filterModel.SearchString) || h.Model.Contains(filterModel.SearchString))
								&& h.ProductionLineId == filterModel.ProductionLineId
								&& (filterModel.ExistedNum.Count == 0 || filterModel.ExistedNum.Count > 0 && filterModel.ExistedNum.Contains(h.NUM))
								&& h.DateTime >= filterModel.FromDate
								&& h.DateTime <= filterModel.ToDate).OrderByDescending(x => x.DateTime);

			return await query.Skip((filterModel.PageNumber - 1) * filterModel.PageSize).Take(filterModel.PageSize).AsNoTracking().ToListAsync();
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
							KT_MIC_WF_SPLs
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
		public async Task<List<KT_MIC_WF_SPL>> GetListByNumAsync(string num)
		{
			return await _context.KT_MIC_WF_SPLs.Where(x => x.NUM.Contains(num)).Include(x => x.ProductionLine).OrderByDescending(x => x.DateTime).ToListAsync();
		}
		public async Task<PaginatedList<KT_MIC_WF_SPL>> GetAllAsync(FilterModel filterModel)
        {
			// Tạo chuỗi truy vấn SQL
			var sqlQuery = @"
							WITH RankedKT_MIC_WF_SPLs AS
							(
								SELECT h.*, 
									   ROW_NUMBER() OVER (PARTITION BY h.NUM ORDER BY h.DateTime DESC) AS RowNum
								FROM KT_MIC_WF_SPLs h
								WHERE h.ProductionLineId = @ProductionLineId
								AND h.DateTime >= @FromDate
								AND h.DateTime <= @ToDate
								AND (@SearchString IS NULL OR h.NUM LIKE '%' + @SearchString + '%' OR h.Model LIKE '%' + @SearchString + '%')
							)
							SELECT *
							FROM RankedKT_MIC_WF_SPLs
							WHERE RowNum = 1
							ORDER BY DateTime DESC
							OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

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
            var items = await _context.KT_MIC_WF_SPLs.FromSqlRaw(sqlQuery, parameters).AsNoTracking().ToListAsync();
			// Đếm số lượng kết quả thỏa mãn điều kiện
			var countQuery = @"
							WITH RankedKT_MIC_WF_SPLs AS
					(
						SELECT h.*, 
							   ROW_NUMBER() OVER (PARTITION BY h.NUM ORDER BY h.DateTime DESC) AS RowNum
						FROM KT_MIC_WF_SPLs h
						WHERE h.ProductionLineId = @ProductionLineId
						AND h.DateTime >= @FromDate
						AND h.DateTime <= @ToDate
						AND (@SearchString IS NULL OR h.NUM LIKE '%' + @SearchString + '%' OR h.Model LIKE '%' + @SearchString + '%')
					)
					SELECT COUNT(*) as Total
					FROM RankedKT_MIC_WF_SPLs
					WHERE RowNum = 1;";

			var counts = await _context.Database.SqlQueryRaw<CountModel>(countQuery, parameters).ToListAsync();
			int count = counts.FirstOrDefault()?.Total ?? 0;
			return new PaginatedList<KT_MIC_WF_SPL>(items, count, filterModel.PageNumber, filterModel.PageSize);
		}

        public async Task<List<KT_MIC_WF_SPL>> GetExistNums(List<string> existNUMs)
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

				var result = _context.KT_MIC_WF_SPLs
					.FromSqlRaw($"SELECT * FROM KT_MIC_WF_SPLs WHERE NUM IN ({existNUMsSetString})", parameters.ToArray())
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

        public async Task<KT_MIC_WF_SPL> GetByIdAsync(long id)
        {
            return await _context.KT_MIC_WF_SPLs.Include(x => x.ProductionLine).FirstOrDefaultAsync(x => x.Id == id);
        }
		public async Task UpdateRangeAsync(IEnumerable<KT_MIC_WF_SPL> entities)
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

		public async Task CreateRangeAsync(IEnumerable<KT_MIC_WF_SPL> entities)
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
		public async Task CreateAsync(KT_MIC_WF_SPL entity)
        {
            _context.KT_MIC_WF_SPLs.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(KT_MIC_WF_SPL entity)
        {
            _context.KT_MIC_WF_SPLs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.KT_MIC_WF_SPLs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

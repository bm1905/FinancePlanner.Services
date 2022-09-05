﻿using System.Data;
using FinancePlanner.TaxServices.Domain.Entities;
using System.Threading.Tasks;
using Dapper;
using FinancePlanner.TaxServices.Infrastructure.Persistence;

namespace FinancePlanner.TaxServices.Infrastructure.Repositories
{
    public class FederalTaxBracketRepository : IFederalTaxBracketRepository
    {
        private readonly IDapperContext _context;

        public FederalTaxBracketRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<PercentageMethodTable> GetFederalTaxPercentage(decimal adjustedAnnualWage, string tableName)
        {
            string query = $"SELECT * FROM {tableName} WHERE {adjustedAnnualWage} >= At_Least AND {adjustedAnnualWage} < But_Less_Than;";
            using IDbConnection connection = _context.CreateConnection();
            PercentageMethodTable percentageMethodTable = await connection.QuerySingleAsync<PercentageMethodTable>(query);
            return percentageMethodTable;
        }
    }
}

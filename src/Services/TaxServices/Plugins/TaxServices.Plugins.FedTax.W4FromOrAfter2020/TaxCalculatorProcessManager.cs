﻿using System.Threading.Tasks;
using FinancePlanner.TaxServices.Application.Features.FederalTax.Queries.GetFederalTaxWithheld;
using FinancePlanner.TaxServices.Application.Services.FederalTaxServices;
using FinancePlanner.TaxServices.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Shared.Models.TaxServices;
using TaxServices.Plugins.FedTax.W4FromOrAfter2020.Models;

namespace TaxServices.Plugins.FedTax.W4FromOrAfter2020
{
    public class TaxCalculatorProcessManager : IFederalTaxServices
    {
        public IConfiguration Configuration { get; }
        private readonly TaxCalculatorManager _taxCalculatorManager;

        public TaxCalculatorProcessManager(IConfiguration configuration, IFederalTaxRepository federalTaxBracketRepository)
        {
            Configuration = configuration;
            _taxCalculatorManager = new TaxCalculatorManager(federalTaxBracketRepository);
        }

        public async Task<GetFederalTaxWithheldQueryResponse> CalculateFederalTaxWithheldAmount(CalculateTaxWithheldRequest model)
        {
            W4FromOrAfter2020Model w4FromOrAfter2020Model = _taxCalculatorManager.GetModel(model);
            decimal adjustedAnnualWage = _taxCalculatorManager.GetAdjustedAnnualWage(w4FromOrAfter2020Model);
            decimal federalTaxWithheldAmount = await _taxCalculatorManager.GetFederalTaxWithheldAmount(w4FromOrAfter2020Model, adjustedAnnualWage);

            GetFederalTaxWithheldQueryResponse response = new GetFederalTaxWithheldQueryResponse
            {
                FederalTaxableWage = model.FederalTaxableWage,
                FederalTaxWithheldAmount = federalTaxWithheldAmount
            };

            return response;
        }
    }
}
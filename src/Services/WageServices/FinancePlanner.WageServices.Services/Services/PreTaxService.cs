﻿using System;
using FinancePlanner.Shared.Models.Exceptions;
using FinancePlanner.Shared.Models.WageServices;

namespace FinancePlanner.WageServices.Services.Services
{
    public class PreTaxService : IPreTaxService
    {
        public PreTaxWagesResponse CalculateTaxableWages(PreTaxWagesRequest request)
        {
            try
            {
                decimal totalGrossPay = 0;

                // Calculate total gross pay
                foreach (WeeklyHour weeklyHour in request.WeeklyHours)
                {
                    decimal rate = weeklyHour.HourlyRate;
                    decimal overTime = (weeklyHour.TotalHours - weeklyHour.TimeOffHours) > 40
                        ? (weeklyHour.TotalHours - weeklyHour.TimeOffHours - 40)
                        : 0;
                    decimal regularHours = weeklyHour.TotalHours - weeklyHour.TimeOffHours - overTime;
                    totalGrossPay += regularHours * rate + weeklyHour.TimeOffHours * rate + overTime * (rate + rate / 2);
                }

                decimal totalPreTaxDeductions = request.Dental +
                                               request.HealthSavingAccount +
                                               request.Medical +
                                               request.Vision +
                                               request.Others +
                                               request.Traditional401KPercentage / 100 * totalGrossPay;


                return new PreTaxWagesResponse()
                {
                    GrossPay = totalGrossPay,
                    TotalPreTaxDeductions = totalPreTaxDeductions,
                    SocialAndMedicareTaxableWages = totalGrossPay - totalPreTaxDeductions + request.Traditional401KPercentage / 100 * totalGrossPay,
                    StateAndFederalTaxableWages = totalGrossPay - totalPreTaxDeductions
                };
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException(ex.Message, ex);
            }
        }
    }
}

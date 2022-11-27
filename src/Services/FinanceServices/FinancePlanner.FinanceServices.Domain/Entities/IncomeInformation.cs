﻿using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.FinanceServices.Domain.Entities;

public class IncomeInformation
{
    [Key]
    public int IncomeInformationId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int PayInformationId { get; set; }
    public decimal GrossPay { get; set; }
    public decimal NetPay { get; set; }
    public decimal TotalHours { get; set; }
    public decimal PayRate { get; set; }
    public decimal TotalPreTaxDeductions { get; set; }
    public decimal TotalPostTaxDeductions { get; set; }
    public decimal StateAndFederalTaxableWages { get; set; }
    public decimal SocialAndMedicareTaxableWages { get; set; }
    public decimal FederalTaxWithheldAmount { get; set; }
    public decimal MedicareWithheldAmount { get; set; }
    public decimal SocialSecurityWithheldAmount { get; set; }
    public decimal StateTaxWithheldAmount { get; set; }
    public decimal TotalTaxesWithheldAmount { get; set; }
}
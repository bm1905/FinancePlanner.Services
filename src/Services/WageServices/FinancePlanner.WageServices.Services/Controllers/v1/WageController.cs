﻿using System.Net;
using FinancePlanner.WageServices.Services.Filters;
using FinancePlanner.WageServices.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.WageServices;

namespace FinancePlanner.WageServices.Services.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ValidateModelFilter]
    public class WageController : ControllerBase
    {
        private readonly IPreTaxService _preTaxService;
        private readonly IPostTaxDeductionService _postTaxService;

        public WageController(IPreTaxService preTaxService, IPostTaxDeductionService postTaxService)
        {
            _preTaxService = preTaxService;
            _postTaxService = postTaxService;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("Test")]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.OK)]
        public IActionResult Index()
        {
            return Ok(new { Status = "V1 Test Passed" });
        }

        [MapToApiVersion("1.0")]
        [HttpPost("CalculateTotalTaxableWages")]
        [ProducesResponseType(typeof(ActionResult<PreTaxWagesResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PreTaxWagesResponse> CalculateTotalTaxableWages([FromBody] PreTaxWagesRequest request)
        {
            PreTaxWagesResponse response = _preTaxService.CalculateTaxableWages(request);
            return Ok(response);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("CalculatePostTaxDeductions")]
        [ProducesResponseType(typeof(ActionResult<PostTaxDeductionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PostTaxDeductionResponse> CalculatePostTaxDeductions([FromBody] PostTaxDeductionRequest request)
        {
            PostTaxDeductionResponse response = _postTaxService.CalculatePostTaxDeductions(request);
            return Ok(response);
        }
    }
}
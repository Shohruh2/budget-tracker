using System.Net;
using CustomResponsePackage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;
using MyBudgetTracker.Services;

namespace MyBudgetTracker.Controllers;

[ApiController]
[Authorize]
public class BudgetsController : ControllerBase
{
    private readonly IBudgetService _service;
    private readonly ICurrentUserService _currentUser;

    public BudgetsController(IBudgetService service, ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    [HttpPost(ApiEndpoints.Budget.Create)]
    public async Task<ActionResult<CustomResponse<BudgetResponse>>> Create([FromBody] CreateBudgetRequest request,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var budgetResponse = await _service.CreateAsync(request, currentUser.Id, token);
        if (budgetResponse == null)
        {
            var errorResponse = CustomResponse<BudgetResponse>.CreateErrorResponse(new ErrorResponse()
            {
                Message = "Budget or category not found in database",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var response = CustomResponse<BudgetResponse>.CreateSuccessResponse(budgetResponse);
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Budget.Get)]
    public async Task<ActionResult<CustomResponse<BudgetResponse>>> Get([FromRoute] Guid id,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var budgetResponse = await _service.GetAsync(id, currentUser.Id, token);
        if (budgetResponse == null)
        {
            var errorResponse = CustomResponse<BudgetResponse>.CreateErrorResponse(new ErrorResponse()
            {
                Message = "Budget or category not found in database",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var response = CustomResponse<BudgetResponse>.CreateSuccessResponse(budgetResponse);
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Budget.GetAll)]
    public async Task<ActionResult<CustomResponse<BudgetsResponse>>> GetAll(CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var budgetsResponse = await _service.GetAllGroupedAsync(currentUser.Id, token);
        if (budgetsResponse == null)
        {
            var errorResponse = CustomResponse<BudgetResponse>.CreateErrorResponse(new ErrorResponse()
            {
                Message = "Budget or category not found in database",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var response = CustomResponse<BudgetsResponse>.CreateSuccessResponse(budgetsResponse);
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Budget.Update)]
    public async Task<ActionResult<CustomResponse<BudgetResponse>>> Update([FromRoute] Guid id,
        [FromBody] UpdateBudgetRequest request, CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var budgetResponse = await _service.UpdateAsync(request, id, currentUser.Id, token);
        if (budgetResponse == null)
        {
            var errorResponse = CustomResponse<BudgetResponse>.CreateErrorResponse(new ErrorResponse()
            {
                Message = "Budget or category not found in database",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var response = CustomResponse<BudgetResponse>.CreateSuccessResponse(budgetResponse);
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Budget.Delete)]
    public async Task<ActionResult<CustomResponse>> Delete([FromRoute] Guid id,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var deletedBudget = await _service.DeleteAsync(id, currentUser.Id, token);
        if (!deletedBudget)
        {
            var errorResponse = CustomResponse<BudgetResponse>.CreateErrorResponse(new ErrorResponse()
            {
                Message = "Budget or category not found in database",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }


        var message = new
        {
            Message = "Deleted successfully"
        };
        var response = CustomResponse.CreateSuccessResponse(message);
        return Ok(response);
    }
    
    [HttpGet(ApiEndpoints.Budget.Statistics)]
    public async Task<ActionResult<CustomResponse<BudgetStatisticsResponse>>> GetBudgetStatistics(CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var budgetStatistics = await _service.GetBudgetStatisticsAsync(currentUser.Id, token);
        if (budgetStatistics == null)
        {
            var errorResponse = CustomResponse<BudgetStatisticsResponse>.CreateErrorResponse(new ErrorResponse
            {
                Message = $"Cannot find budget statistics",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var response = CustomResponse<BudgetStatisticsResponse>.CreateSuccessResponse(budgetStatistics);
        return Ok(response);
    }
}
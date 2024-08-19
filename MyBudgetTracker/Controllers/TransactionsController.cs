using System.Net;
using CustomResponsePackage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetTracker.Mapping;
using MyBudgetTracker.Models;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;
using MyBudgetTracker.Services;

namespace MyBudgetTracker.Controllers;

[ApiController]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;
    private readonly ICurrentUserService _currentUser;

    public TransactionsController(ITransactionService service, ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    [HttpPost(ApiEndpoints.Transaction.Create)]
    public async Task<ActionResult<CustomResponse<TransactionResponse>>> Create([FromBody] CreateTransactionRequest request,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();
        var transactionResponse = await _service.CreateAsync(request, currentUser.Id, token);
        if (transactionResponse == null)
        {
            var errorResponse = CustomResponse<TransactionResponse>.CreateErrorResponse(new ErrorResponse()
            { 
                Message = "Category id not found", 
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }
        
        var response = CustomResponse<TransactionResponse>.CreateSuccessResponse(transactionResponse);
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Transaction.GetAll)]
    public async Task<ActionResult<CustomResponse<TransactionsResponse>>> GetAll(CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var transactionResponse = await _service.GetAllGroupedAsync(currentUser.Id, token);
        var response = CustomResponse<TransactionsResponse>.CreateSuccessResponse(transactionResponse);
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Transaction.Get)]
    public async Task<ActionResult<CustomResponse<TransactionResponse>>> Get([FromRoute] Guid id,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var transactionResponse = await _service.GetAsync(id, currentUser.Id, token);
        if (transactionResponse == null)
        {
            var errorResponse = CustomResponse<TransactionResponse>.CreateErrorResponse(new ErrorResponse()
            {
                Message = "Can't find transaction",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var response = CustomResponse<TransactionResponse>.CreateSuccessResponse(transactionResponse);
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Transaction.Update)]
    public async Task<ActionResult<CustomResponse<TransactionResponse>>> Update([FromRoute] Guid id,
        [FromBody] UpdateTransactionRequest request,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();
        
        var transactionResponse = await _service.UpdateAsync(id, request, currentUser.Id, token);
        if (transactionResponse == null)
        {
            var errorResponse = CustomResponse<TransactionResponse>.CreateErrorResponse(new ErrorResponse()
            { 
                Message = "Transaction not found", 
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var response = CustomResponse<TransactionResponse>.CreateSuccessResponse(transactionResponse);
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Transaction.Delete)]
    public async Task<ActionResult<CustomResponse>> Delete([FromRoute] Guid id, CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var deleted = await _service.DeleteAsync(id, currentUser.Id, token);
        if (!deleted)
        {
            var errorResponse = CustomResponse<TransactionResponse>.CreateErrorResponse(new ErrorResponse()
            { 
                Message = "Transaction not found", 
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var responseMessage = new
        {
            Message = "Deleted successfully"
        };

        var response = CustomResponse.CreateSuccessResponse(responseMessage);
        return Ok(response);
    }
}
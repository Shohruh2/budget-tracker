using System.Net;
using CustomResponsePackage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetTracker.Mapping;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;
using MyBudgetTracker.Services;

namespace MyBudgetTracker.Controllers;


[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ICurrentUserService _currentUser;

    public CategoriesController(ICategoryService categoryService, ICurrentUserService currentUser)
    {
        _categoryService = categoryService;
        _currentUser = currentUser;
    }

    [Authorize]
    [HttpPost(ApiEndpoints.Category.Create)]
    public async Task<ActionResult<CustomResponse<CategoryResponse>>> Create([FromBody] CreateCategoryDto request,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();

        var category = await _categoryService.CreateAsync(request, currentUser.Id, token);
        var categoryResponse = category.MapToResponse();
        var response = CustomResponse<CategoryResponse>.CreateSuccessResponse(categoryResponse);
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Category.Get)]
    public async Task<ActionResult<CustomResponse<CategoryResponse>>> Get([FromRoute] Guid id,
        CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();
        
        var category = await _categoryService.GetAsync(id, currentUser.Id, token);
        if (category == null)
        {
            var errorResponse = CustomResponse<CategoryResponse>.CreateErrorResponse(new ErrorResponse
            {
                Message = $"Can't find in database ",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var categoryResponse = category.MapToResponse();
        var response = CustomResponse<CategoryResponse>.CreateSuccessResponse(categoryResponse);
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Category.GetAll)]
    public async Task<ActionResult<CustomResponse<CategoriesResponse>>> GetAll(CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();
        
        var categories = await _categoryService.GetAllAsync(currentUser.Id, token);
        var categoriesResponse = categories.MapToResponse();
        var response = CustomResponse<CategoriesResponse>.CreateSuccessResponse(categoriesResponse);
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Category.Delete)]
    public async Task<ActionResult<CustomResponse>> Delete([FromRoute] Guid id, CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();
        
        var deleted = await _categoryService.DeleteAsync(id, currentUser.Id, token);
        if (!deleted)
        {
            var errorResponse = CustomResponse.CreateErrorResponse(new ErrorResponse
            {
                Message = $"Can't find category in database ",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }

        var responseMessage = new
        {
            Message = "Category deleted successfully"
        };
        
        var response = CustomResponse.CreateSuccessResponse(responseMessage);
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Category.Update)]
    public async Task<ActionResult<CustomResponse<CategoryResponse>>> Update([FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest request, CancellationToken token)
    {
        var currentUser = _currentUser.GetCurrentUser();
        var existingCategory = await _categoryService.GetAsync(id, currentUser.Id, token);
        if (existingCategory == null)
        {
            var errorResponse = CustomResponse<CategoryResponse>.CreateErrorResponse(new ErrorResponse
            {
                Message = $"Can't find category in database",
                Code = HttpStatusCode.NotFound.ToString()
            });

            return NotFound(errorResponse);
        }
        
        var category = request.MapToCategory(id, currentUser.Id, existingCategory);
        var updatedCategory = await _categoryService.UpdateAsync(category, token);

        var categoryResponse = updatedCategory.MapToResponse();
        var response = CustomResponse<CategoryResponse?>.CreateSuccessResponse(categoryResponse);
        return Ok(response);
    }
}
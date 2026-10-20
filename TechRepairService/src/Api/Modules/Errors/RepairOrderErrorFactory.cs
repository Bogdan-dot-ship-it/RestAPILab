using Application.RepairOrders.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class RepairOrderErrorFactory
{
    public static ObjectResult ToObjectResult(this RepairOrderException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                RepairOrderAlreadyExistsException => StatusCodes.Status409Conflict,
                RepairOrderNotFoundException => StatusCodes.Status404NotFound,
                RepairOrderUnhandledException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Repair order error handler is not implemented.")
            }
        };
    }
}
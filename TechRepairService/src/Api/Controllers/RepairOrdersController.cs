using Api.Dtos;
using Application.RepairOrders.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("repair-orders")]
[ApiController]
public class RepairOrdersController(IRepairOrderService repairOrderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RepairOrderDto>>> GetRepairOrders(CancellationToken cancellationToken)
    {
        var orders = await repairOrderService.GetRepairOrders(cancellationToken);
        var dtos = orders.Select(RepairOrderDto.FromDomainModel).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RepairOrderDto>> GetRepairOrder(Guid id, CancellationToken cancellationToken)
    {
        var order = await repairOrderService.GetRepairOrder(id, cancellationToken);
        if (order is null)
        {
            return NotFound();
        }

        return Ok(RepairOrderDto.FromDomainModel(order));
    }

    [HttpPost]
    public async Task<ActionResult<RepairOrderDto>> CreateRepairOrder(
        [FromBody] CreateRepairOrderDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await repairOrderService.Add(
                request.OrderNumber,
                request.DeviceModel,
                request.EstimatedCost,
                request.IsUrgent,
                request.Status,
                request.EstimatedCompletionDate,
                cancellationToken);

            var dto = RepairOrderDto.FromDomainModel(order);
            return CreatedAtAction(nameof(GetRepairOrder), new { id = dto.Id }, dto);
        }
        catch (ArgumentException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RepairOrderDto>> UpdateRepairOrder(
        Guid id,
        [FromBody] UpdateRepairOrderDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await repairOrderService.Update(
                id,
                request.OrderNumber,
                request.DeviceModel,
                request.EstimatedCost,
                request.IsUrgent,
                request.Status,
                request.EstimatedCompletionDate,
                cancellationToken);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(RepairOrderDto.FromDomainModel(order));
        }
        catch (ArgumentException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRepairOrder(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await repairOrderService.Delete(id, cancellationToken);
        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.RepairOrders.Commands;
using Domain.RepairOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("repair-orders")]
[ApiController]
public class RepairOrdersController(
    ISender sender,
    IRepairOrderQueries repairOrderQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RepairOrderDto>>> GetRepairOrders(CancellationToken cancellationToken)
    {
        var orders = await repairOrderQueries.GetAll(cancellationToken);
        return Ok(orders.Select(RepairOrderDto.FromDomainModel).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RepairOrderDto>> GetRepairOrder(Guid id, CancellationToken cancellationToken)
    {
        var order = await repairOrderQueries.GetById(new RepairOrderId(id), cancellationToken);

        return order.Match<ActionResult<RepairOrderDto>>(
            p => Ok(RepairOrderDto.FromDomainModel(p)),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<RepairOrderDto>> CreateRepairOrder(
        [FromBody] CreateRepairOrderDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRepairOrderCommand
        {
            OrderNumber = request.OrderNumber,
            DeviceModel = request.DeviceModel,
            EstimatedCost = request.EstimatedCost,
            IsUrgent = request.IsUrgent,
            Status = request.Status,
            EstimatedCompletionDate = request.EstimatedCompletionDate,
            DeviceId = request.DeviceId,
            TechnicianId = request.TechnicianId
        };

        var result = await sender.Send(command, cancellationToken);

        return result.Match<ActionResult<RepairOrderDto>>(
            p => CreatedAtAction(nameof(GetRepairOrder), new { id = p.Id.Value }, RepairOrderDto.FromDomainModel(p)),
            e => e.ToObjectResult());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RepairOrderDto>> UpdateRepairOrder(
        Guid id,
        [FromBody] UpdateRepairOrderDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRepairOrderCommand
        {
            Id = id,
            OrderNumber = request.OrderNumber,
            DeviceModel = request.DeviceModel,
            EstimatedCost = request.EstimatedCost,
            IsUrgent = request.IsUrgent,
            Status = request.Status,
            EstimatedCompletionDate = request.EstimatedCompletionDate,
            DeviceId = request.DeviceId,
            TechnicianId = request.TechnicianId
        };

        var result = await sender.Send(command, cancellationToken);

        return result.Match<ActionResult<RepairOrderDto>>(
            p => Ok(RepairOrderDto.FromDomainModel(p)),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRepairOrder(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteRepairOrderCommand { Id = id };
        var result = await sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(
            _ => NoContent(),
            e => e.ToObjectResult());
    }
}
using Application.Common.Interfaces.Repositories;
using Application.RepairOrders.Exceptions;
using Domain.RepairOrders;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.RepairOrders.Commands;

public record UpdateRepairOrderCommand : IRequest<Either<RepairOrderException, RepairOrder>>
{
    public required Guid Id { get; init; }
    public required string OrderNumber { get; init; }
    public required string DeviceModel { get; init; }
    public required decimal EstimatedCost { get; init; }
    public required bool IsUrgent { get; init; }
    public required RepairStatus Status { get; init; }
    public required DateTime EstimatedCompletionDate { get; init; }
    public Guid? DeviceId { get; init; }
    public Guid? TechnicianId { get; init; }
}

public class UpdateRepairOrderCommandHandler(IRepairOrderRepository repairOrderRepository)
    : IRequestHandler<UpdateRepairOrderCommand, Either<RepairOrderException, RepairOrder>>
{
    public async Task<Either<RepairOrderException, RepairOrder>> Handle(
        UpdateRepairOrderCommand request,
        CancellationToken cancellationToken)
    {
        var repairOrderId = new RepairOrderId(request.Id);
        var orderOption = await repairOrderRepository.GetById(repairOrderId, cancellationToken);

        return await orderOption.MatchAsync(
            order => CheckDuplicates(order.Id, request.OrderNumber, cancellationToken)
                .BindAsync(_ => UpdateEntity(order, request, cancellationToken)),
            () => new RepairOrderNotFoundException(repairOrderId));
    }

    private async Task<Either<RepairOrderException, Unit>> CheckDuplicates(
        RepairOrderId currentId,
        string orderNumber,
        CancellationToken cancellationToken)
    {
        var existingOrder = await repairOrderRepository.GetByOrderNumber(orderNumber, cancellationToken);

        return existingOrder.Match<Either<RepairOrderException, Unit>>(
            order => order.Id == currentId ? Unit.Default : new RepairOrderAlreadyExistsException(order.Id),
            () => Unit.Default);
    }

    private async Task<Either<RepairOrderException, RepairOrder>> UpdateEntity(
        RepairOrder order,
        UpdateRepairOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            order.UpdateDetails(
                request.DeviceModel,
                request.EstimatedCost,
                request.IsUrgent,
                request.Status,
                request.EstimatedCompletionDate,
                request.DeviceId,
                request.TechnicianId);

            return await repairOrderRepository.Update(order, cancellationToken);
        }
        catch (Exception exception)
        {
            return new RepairOrderUnhandledException(order.Id, exception);
        }
    }
}
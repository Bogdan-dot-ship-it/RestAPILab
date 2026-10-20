using Application.Common.Interfaces.Repositories;
using Application.RepairOrders.Exceptions;
using Domain.RepairOrders;
using LanguageExt;
using MediatR;

namespace Application.RepairOrders.Commands;

public record CreateRepairOrderCommand : IRequest<Either<RepairOrderException, RepairOrder>>
{
    public required string OrderNumber { get; init; }
    public required string DeviceModel { get; init; }
    public required decimal EstimatedCost { get; init; }
    public required bool IsUrgent { get; init; }
    public required RepairStatus Status { get; init; }
    public required DateTime EstimatedCompletionDate { get; init; }
    public Guid? DeviceId { get; init; }
    public Guid? TechnicianId { get; init; }
}

public class CreateRepairOrderCommandHandler(IRepairOrderRepository repairOrderRepository)
    : IRequestHandler<CreateRepairOrderCommand, Either<RepairOrderException, RepairOrder>>
{
    public async Task<Either<RepairOrderException, RepairOrder>> Handle(
        CreateRepairOrderCommand request,
        CancellationToken cancellationToken)
    {
        var existingOrder = await repairOrderRepository.GetByOrderNumber(request.OrderNumber, cancellationToken);

        return await existingOrder.MatchAsync(
            order => new RepairOrderAlreadyExistsException(order.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<RepairOrderException, RepairOrder>> CreateEntity(
        CreateRepairOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = RepairOrder.New(
                RepairOrderId.New(),
                request.OrderNumber,
                request.DeviceModel,
                request.EstimatedCost,
                request.IsUrgent,
                request.Status,
                request.EstimatedCompletionDate,
                request.DeviceId,
                request.TechnicianId);

            return await repairOrderRepository.Add(order, cancellationToken);
        }
        catch (Exception exception)
        {
            return new RepairOrderUnhandledException(RepairOrderId.Empty(), exception);
        }
    }
}
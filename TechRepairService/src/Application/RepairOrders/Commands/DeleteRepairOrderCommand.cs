using Application.Common.Interfaces.Repositories;
using Application.RepairOrders.Exceptions;
using Domain.RepairOrders;
using LanguageExt;
using MediatR;

namespace Application.RepairOrders.Commands;

public record DeleteRepairOrderCommand : IRequest<Either<RepairOrderException, RepairOrder>>
{
    public required Guid Id { get; init; }
}

public class DeleteRepairOrderCommandHandler(IRepairOrderRepository repairOrderRepository)
    : IRequestHandler<DeleteRepairOrderCommand, Either<RepairOrderException, RepairOrder>>
{
    public async Task<Either<RepairOrderException, RepairOrder>> Handle(
        DeleteRepairOrderCommand request,
        CancellationToken cancellationToken)
    {
        var repairOrderId = new RepairOrderId(request.Id);
        var orderOption = await repairOrderRepository.GetById(repairOrderId, cancellationToken);

        return await orderOption.MatchAsync(
            order => DeleteEntity(order, cancellationToken),
            () => new RepairOrderNotFoundException(repairOrderId));
    }

    private async Task<Either<RepairOrderException, RepairOrder>> DeleteEntity(
        RepairOrder order,
        CancellationToken cancellationToken)
    {
        try
        {
            await repairOrderRepository.Delete(order, cancellationToken);
            return order;
        }
        catch (Exception exception)
        {
            return new RepairOrderUnhandledException(order.Id, exception);
        }
    }
}
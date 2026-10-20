using FluentValidation;

namespace Application.RepairOrders.Commands;

public class CreateRepairOrderCommandValidator : AbstractValidator<CreateRepairOrderCommand>
{
    public CreateRepairOrderCommandValidator()
    {
        RuleFor(x => x.OrderNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.DeviceModel)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.EstimatedCost)
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(10, 2, false);

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.EstimatedCompletionDate)
            .NotEmpty();
    }
}
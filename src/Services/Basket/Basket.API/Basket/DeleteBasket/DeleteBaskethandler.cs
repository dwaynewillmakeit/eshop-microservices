
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeletebasketResult>;

public record DeletebasketResult(bool IsSuccess);

public class DeletebasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeletebasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is Required");
    }
}


public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeletebasketResult>
{
    public async Task<DeletebasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {

        return new DeletebasketResult(true);

    }
}

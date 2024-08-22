

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand (ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string userName);

public class StoreBasketCommandValadator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValadator()
    {
        RuleFor(x => x.Cart).NotNull().NotEmpty().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
       ShoppingCart cart = command.Cart;

        return new StoreBasketResult("test name");
    }
}

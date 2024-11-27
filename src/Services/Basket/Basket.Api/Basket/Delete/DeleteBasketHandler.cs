using BuildingBlocks.Cqrs;

namespace Basket.Api.Basket.Delete
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var res = await basketRepository.DeleteBasket(request.Username);
            return new DeleteBasketResult(res);
        }
    }
}
using Core.Models;

namespace Core.Contracts.Response
{
    public record ImageResponse(Guid Id, int Order, Guid ProductId);
}
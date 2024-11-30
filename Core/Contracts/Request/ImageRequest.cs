using Core.Models;

namespace Core.Contracts.Request
{
    public record ImageRequest(int Order, Guid ProductId);
}
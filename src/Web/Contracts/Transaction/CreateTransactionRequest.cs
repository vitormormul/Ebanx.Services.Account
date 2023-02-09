using System.Text.Json.Serialization;
using Ebanx.Services.Account.Domain.Transaction.Entities;

#pragma warning disable CS1591
namespace Ebanx.Services.Account.Web.Contracts.Transaction;

public record CreateTransactionRequest
{
    public CreateTransactionRequest(int amount, string? destinationAccountId, string? originAccountId, string type)
    {
        Amount = amount;
        DestinationAccountId = destinationAccountId;
        OriginAccountId = originAccountId;
        Type = type;
    }

    [JsonPropertyName("amount")] public int Amount { get; }

    [JsonPropertyName("destination")] public string? DestinationAccountId { get; }

    [JsonPropertyName("origin")] public string? OriginAccountId { get; }

    [JsonPropertyName("type")] public string? Type { get; }
}
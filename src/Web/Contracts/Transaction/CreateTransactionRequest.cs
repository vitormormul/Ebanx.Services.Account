using System.Text.Json.Serialization;
using Ebanx.Services.Account.Domain.Transaction.Entities;

#pragma warning disable CS1591
namespace Ebanx.Services.Account.Web.Contracts.Transaction;

public record CreateTransactionRequest
{
    public CreateTransactionRequest(int amount, string? destinationAccount, string? originAccount, TransactionType type)
    {
        Amount = amount;
        DestinationAccount = destinationAccount;
        OriginAccount = originAccount;
        Type = type;
    }

    [JsonPropertyName("amount")] public int Amount { get; }

    [JsonPropertyName("destination")] public string? DestinationAccount  { get; }

    [JsonPropertyName("origin")] public string? OriginAccount  { get; }

    [JsonPropertyName("type")] public TransactionType Type  { get; }
}
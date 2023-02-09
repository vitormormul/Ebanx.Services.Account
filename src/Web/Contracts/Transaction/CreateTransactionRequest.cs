using System.Text.Json.Serialization;
using Ebanx.Services.Account.Domain.Transaction.Entities;

#pragma warning disable CS1591
namespace Ebanx.Services.Account.Web.Contracts.Transaction;

public record CreateTransactionRequest
{
    [JsonPropertyName("amount")] public int Amount { get; set; }

    [JsonPropertyName("destination")] public string DestinationAccountId { get; set; } = String.Empty;

    [JsonPropertyName("origin")] public string OriginAccountId { get; set; } = String.Empty;

    [JsonPropertyName("type")] public TransactionType Type { get; set; }
}
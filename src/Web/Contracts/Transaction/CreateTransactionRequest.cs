using System.Text.Json.Serialization;
using Ebanx.Services.Account.Domain.Account.ValueObjects;
using Ebanx.Services.Account.Domain.Transaction.Entities;

#pragma warning disable CS1591
namespace Ebanx.Services.Account.Web.Contracts.Transaction;

public record CreateTransactionRequest()
{
    [JsonPropertyName("type")]
    public TransactionType Type;
    [JsonPropertyName("amount")]
    public int Amount;
    [JsonPropertyName("origin")]
    public AccountId? OriginAccount;
    [JsonPropertyName("destination")]
    public AccountId? DestinationAccount;

};
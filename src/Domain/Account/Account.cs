namespace Ebanx.Services.Account.Domain.Account;

public record Account
{
    public Account(string id, int balance)
    {
        Id = id;
        Balance = balance;
    }

    public string Id { get; set; }
    public int Balance { get; set; }

    public int Deposit(int amount)
    {
        Balance += amount;
        return Balance;
    }

    public int Withdraw(int amount)
    {
        Balance -= amount;
        return Balance;
    }
}
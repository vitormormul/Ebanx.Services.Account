using Microsoft.EntityFrameworkCore;

namespace Ebanx.Services.Account.Infrastructure.Persistence;

public class AccountDbContext : DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Account.Account> Accounts { get; set; }
}
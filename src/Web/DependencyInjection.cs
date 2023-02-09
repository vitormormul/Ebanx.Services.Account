using System.Text.Json.Serialization;
using Ebanx.Services.Account.Application.Account.Commands.CreateAccount;
using Ebanx.Services.Account.Application.Common.Behavior;
using Ebanx.Services.Account.Application.Common.Interfaces.Services;
using Ebanx.Services.Account.Infrastructure.Interfaces;
using Ebanx.Services.Account.Infrastructure.Persistence;
using Ebanx.Services.Account.Infrastructure.Persistence.Repositories;
using Ebanx.Services.Account.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS1591
namespace Ebanx.Services.Account.Web;

public static class DependencyInjection
{
    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        #region Web

        serviceCollection.AddControllers()
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        serviceCollection.AddRouting(opt => opt.LowercaseUrls = true);

        #endregion

        #region Application

        var applicationAssembly = typeof(CreateAccountCommand).Assembly;

        serviceCollection.AddMediatR(applicationAssembly);
        serviceCollection.AddValidatorsFromAssembly(applicationAssembly);
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        #endregion

        #region Infrastructure

        serviceCollection.AddDbContext<AccountDbContext>(opt =>
            opt.UseInMemoryDatabase(AccountDbContext.DatabaseName));

        serviceCollection.AddTransient<IAccountRepository, AccountRepository>();
        serviceCollection.AddTransient<IAccountWriter, AccountWriter>();
        serviceCollection.AddTransient<IAccountReader, AccountReader>();

        #endregion
    }
}
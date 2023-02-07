using MediatR;

#pragma warning disable CS1591
namespace Ebanx.Services.Account.Web;

public static class DependencyInjection
{
    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        #region Base
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        serviceCollection.AddRouting(opt => opt.LowercaseUrls = true);
        #endregion

        #region Application
        serviceCollection.AddMediatR(typeof(Ebanx.Services.Account.Application.Account.Common.AccountResult).Assembly);
        #endregion
    }
}
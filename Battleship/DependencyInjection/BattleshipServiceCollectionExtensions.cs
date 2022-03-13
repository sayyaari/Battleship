using Battleship.Factories;
using Battleship.Services;
using Battleship.Validators;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BattleshipServiceCollectionExtensions
    {
        public static IServiceCollection AddBattleshipServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IBoardFactory, BoardFactory>()
                .AddSingleton<IBoardGridFactory, BoardGridFactory>()
                .AddSingleton<ICellFactory, CellFactory>()
                .AddSingleton<IPositionValidator, PositionValidator>()
                .AddSingleton<IPositionGenerator, PositionGenerator>();


            return services;
        }

    }
}

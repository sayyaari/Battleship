using Battleship.Factories;
using Battleship.Services;
using Battleship.Validators;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Battleship.Tests.DependencyInjection
{
    public class BattleshipServiceCollectionExtensionsTests
    {

        [Theory]
        [InlineData(typeof(IBoardFactory), typeof(BoardFactory))]
        [InlineData(typeof(IBoardGridFactory), typeof(BoardGridFactory))]
        [InlineData(typeof(ICellFactory), typeof(CellFactory))]
        [InlineData(typeof(IPositionValidator), typeof(PositionValidator))]
        [InlineData(typeof(IPositionGenerator), typeof(PositionGenerator))]
        public void Should_ServiceProvider_Return_Service(Type serviceType, Type implementationType)
        {
            ServiceCollection services = new ();
            services.AddBattleshipServices();
            var serviceProvider = services.BuildServiceProvider();

            var serviceInstance = serviceProvider.GetRequiredService(serviceType);

            serviceInstance.Should().BeOfType(implementationType);
        }
    }
}

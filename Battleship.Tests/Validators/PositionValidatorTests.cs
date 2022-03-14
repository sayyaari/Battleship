using AutoFixture;
using Battleship.Model;
using Battleship.Tests.Utils;
using Battleship.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Battleship.Tests.Validators
{
    public class PositionValidatorTests
    {
        private readonly int _width;
        private readonly int _height;
        private readonly BoardDimension _dimension;
        private readonly PositionValidator _positionValidator;
        private readonly Random _random;

        public PositionValidatorTests()
        {
            _width = Generator.GenerateNumber(2,10);
            _height = Generator.GenerateNumber(2, 10);

            _dimension = new BoardDimension(_width, _height);
            _positionValidator = new PositionValidator();
            _random = new Random();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(0, null)]
        [InlineData(null, 0)]
        [InlineData(0, 0)]
        public void Should_Include_The_Position(int? x, int? y)
        {

            Position position = new(x ?? _random.Next(0, _width - 1), y ?? _random.Next(0, _height - 1));

            var result = _positionValidator.IsValid(position, _dimension);

            result.Should().BeTrue();

        }

        [Fact]
        public void Should_Not_Include_Negative_X()
        {
            Position position = new(-_random.Next(1, _width - 1), _random.Next(0,_height));

            var result = _positionValidator.IsValid(position, _dimension);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Not_Include_Negative_Y()
        {
            Position position = new(_random.Next(0, _width - 1), -_random.Next(1, _height - 1));

            var result = _positionValidator.IsValid(position, _dimension);

            result.Should().BeFalse();
        }

        [Fact]
        public void Should_Not_Include()
        {
            Position position = new(_random.Next(_width + 1, int.MaxValue), -_random.Next(_height + 1, int.MaxValue));

            var result = _positionValidator.IsValid(position, _dimension);

            result.Should().BeFalse();
        }
    }
}

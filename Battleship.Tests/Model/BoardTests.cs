using AutoFixture;
using AutoFixture.AutoMoq;
using Battleship.Model;
using Battleship.Validators;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Battleship.Tests.Model
{
    public class BoardTests
    {
        private readonly IFixture _fixture;
        private Board _board;
        private Mock<IBoardGrid> _boardGrid;
        private Mock<ICell> _cellUnderAttack;

        public BoardTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }


        [Fact]
        public void Should_Construction_Set_The_Grid()
        {
            _boardGrid = new Mock<IBoardGrid>();

            _board = new Board(_boardGrid.Object);

            _board.Grid.Should().Be(_boardGrid.Object);
        }


        [Theory]
        [InlineData(AttackResult.Miss)]
        [InlineData(AttackResult.Hit)]
        public void Should_Attack_Return_ExpectedResult(AttackResult expectedAttackResult)
        {
            Position attackPosition = _fixture.Create<Position>();

            SetupBoardGridForAttack(expectedAttackResult, attackPosition);

            _board = new Board(_boardGrid.Object);


            var attackResult = _board.TakeAttack(attackPosition.X, attackPosition.Y);



            attackResult.Should().Be(expectedAttackResult);

            _cellUnderAttack.VerifyAll();

            _boardGrid.VerifyAll();
        }


        [Fact]
        public void Should_Attack_Throw_Exception_When_Attack_Position_Is_Invalid()
        {
            ICell returnedCellObject = null;

            _boardGrid = new Mock<IBoardGrid>(MockBehavior.Strict);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            _boardGrid.Setup(x => x.TryGet(It.IsAny<Position>(), out returnedCellObject))
                .Returns(false);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            Position attackPosition = _fixture.Create<Position>();

            _board = new Board(_boardGrid.Object);



            Action act = () => _board.TakeAttack(attackPosition.X, attackPosition.Y);



            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"The provided position {attackPosition} is not in the board's boundary");

            _boardGrid.VerifyAll();
        }


        private void SetupBoardGridForAttack(AttackResult expectedAttackResult, Position attackPosition)
        {
            // Set up the Cell taking attack
            _cellUnderAttack = new Mock<ICell>(MockBehavior.Strict);
            _cellUnderAttack.Setup(x => x.Attack()).Returns(expectedAttackResult);


            // Set up BoardGrid
            ICell returnedCellObject = null;
            _boardGrid = new Mock<IBoardGrid>(MockBehavior.Strict);
            _boardGrid.Setup(x => x.TryGet(It.IsAny<Position>(), out returnedCellObject))
                .Callback((Position p, out ICell cell) =>
                {
                    p.Should().Be(attackPosition);
                    cell = this._cellUnderAttack.Object;
                })
                .Returns(true);
        }


    }
}

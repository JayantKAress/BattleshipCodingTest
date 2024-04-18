using AutoFixture.Xunit2;
using BattleShipStateTracker.Common.Models;
using BattleShipStateTracker.Components.BattleShip;
using BattleshipUnitTest.CustomTestAttributes;
using FluentAssertions;
using Moq;

namespace BattleshipUnitTest.Modules.Command
{
    public class CreateBoardCommand_UnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ValidInput_CreateBoard(
            [Frozen] Mock<CreateBoardCommandHandler> sut, CreateBoardCommand command)
        {
            // Arrange            
            //command.BoardSize = 5;

            // Act           
            var result = await sut.Object.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ResponseModel<bool>>();
        }
    }
}
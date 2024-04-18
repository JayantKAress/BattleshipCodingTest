using AutoFixture.Xunit2;
using BattleShipCodingTest.Shared.Exceptions;
using BattleShipStateTracker.Common.Models;
using BattleShipStateTracker.Common.Service;
using BattleShipStateTracker.Components.BattleShip.Command;
using BattleshipUnitTest.CustomTestAttributes;
using FluentAssertions;
using Moq;

namespace BattleShipUnitTest.Modules.Command
{
    public class FireShipCommand_UnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_InvalidInput_FireShip_ThrowsException(
           [Frozen] Mock<FireShipCommandHandler> sut, FireShipCommand command)
        {
            /// Arrange            
            command.Xcordinate = 10;
            command.Ycordinate = 10;

            // Assert
            await Assert.ThrowsAsync<BattleShipApiException>(() => sut.Object.Handle(command, default));
        }

        [Theory, AutoMoqData]
        public async Task Handle_ValidInput_AddShip(
            [Frozen] Mock<FireShipCommandHandler> sut, FireShipCommand command,
            [Frozen] Mock<IBattleShipService> battleShipService)
        {
            // Arrange            
            command.Xcordinate = 0;
            command.Ycordinate = 1;

            battleShipService.Setup(x => x.FireShip(command.Xcordinate, command.Ycordinate));
            // Act           
            var result = await sut.Object.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ResponseModel<bool>>();
        }
    }
}

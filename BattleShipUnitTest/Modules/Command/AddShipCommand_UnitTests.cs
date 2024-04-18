﻿using AutoFixture.Xunit2;
using BattleShipCodingTest.Shared.Exceptions;
using BattleShipStateTracker.Common.Models;
using BattleShipStateTracker.Common.Service;
using BattleShipStateTracker.Components.BattleShip;
using BattleshipUnitTest.CustomTestAttributes;
using FluentAssertions;
using Moq;

namespace BattleshipUnitTest.Modules.Command
{
    public class AddShipCommand_UnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_InvalidInput_AddShip_ThrowsException(
            [Frozen] Mock<AddShipCommandHandler> sut, AddShipCommand command)
        {
            /// Arrange            
            command.TotalShips = 0;

            // Assert
            await Assert.ThrowsAsync<BattleShipApiException>(() => sut.Object.Handle(command, default));
        }

        [Theory, AutoMoqData]
        public async Task Handle_InvalidInput_AddShipOutsideBoard_ThrowsException(
            [Frozen] Mock<AddShipCommandHandler> sut, AddShipCommand command, int totalships)
        {
            /// Arrange            
            command.TotalShips = totalships;

            // Assert
            await Assert.ThrowsAsync<BattleShipApiException>(() => sut.Object.Handle(command, default));
        }

        [Theory, AutoMoqData]
        public async Task Handle_ValidInput_AddShip(
            [Frozen] Mock<AddShipCommandHandler> sut, AddShipCommand command,
            [Frozen] Mock<IBattleShipService> battleShipService)
        {
            // Arrange            
            command.TotalShips = 2;
            battleShipService.Setup(x => x.AddShip(command.TotalShips));
            // Act           
            var result = await sut.Object.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ResponseModel<bool>>();
        }

    }
}

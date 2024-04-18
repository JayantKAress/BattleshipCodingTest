using BattleShipCodingTest.Shared.Exceptions;
using BattleShipStateTracker.Common.Models;
using BattleShipStateTracker.Common.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Components.BattleShip.Command
{
    public class FireShipCommand : IRequest<ResponseModel<bool>>
    {
        public int Xcordinate { get; set; }
        public int Ycordinate { get; set; }
    }
    public class FireShipCommandHandler : IRequestHandler<FireShipCommand, ResponseModel<bool>>
    {
        private readonly IBattleShipService _battleShipService;
        private const int maxCoordinate = 9;
        public FireShipCommandHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<ResponseModel<bool>> Handle(FireShipCommand request, CancellationToken cancellationToken)
        {
            if (request.Xcordinate > maxCoordinate || request.Ycordinate > maxCoordinate)
            {
                throw new BattleShipApiException($"x and y co-ordinates cannot be more than {maxCoordinate}");
            }

            _battleShipService.FireShip(request.Xcordinate, request.Ycordinate);
            var message = _battleShipService.HitOrMissShip();
            if (!_battleShipService.WinGame())
            {
                message = "Great! You won the game!!";
            }
            return new ResponseModel<bool>(message);
        }
    }
}

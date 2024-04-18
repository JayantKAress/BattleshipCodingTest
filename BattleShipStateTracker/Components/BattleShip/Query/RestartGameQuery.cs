using BattleShipStateTracker.Common.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Components.BattleShip.Query
{
    public class RestartGameQuery : IRequest<string> { }

    public class RestartGameQueryHandler : IRequestHandler<RestartGameQuery, string>
    {
        private readonly IBattleShipService _battleShipService;
        public RestartGameQueryHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }
        public async Task<string> Handle(RestartGameQuery request, CancellationToken cancellationToken)
        {
            _battleShipService.RestartGame();
            return "Game Restarted, please add new board and ships again";
        }
    }
}

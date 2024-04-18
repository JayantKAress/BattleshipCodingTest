using BattleShipStateTracker.Common.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Components.BattleShip.Query
{
    public class ShowBoardQuery : IRequest<string>
    {
        public class ShowBoardQueryHandler : IRequestHandler<ShowBoardQuery, string>
        {
            private readonly IBattleShipService _battleShipService;
            public ShowBoardQueryHandler(IBattleShipService battleShipService)
            {
                _battleShipService = battleShipService;
            }

            public async Task<string> Handle(ShowBoardQuery request, CancellationToken cancellationToken)
            {
                return _battleShipService.ShowBoard();
            }
        }
    }
}

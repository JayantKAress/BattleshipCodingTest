using BattleShipStateTracker.Common.Models;
using BattleShipStateTracker.Common.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Components.BattleShip
{
    public class AddShipCommand : IRequest<ResponseModel<bool>>
    {
        public int TotalShips { get; set; }
    }
    public class AddShipCommandHandler : IRequestHandler<AddShipCommand, ResponseModel<bool>>
    {
        private readonly IBattleShipService _battleShipService;
        public AddShipCommandHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<ResponseModel<bool>> Handle(AddShipCommand request, CancellationToken cancellationToken)
        {
            _battleShipService.AddShip(request.TotalShips);
            return new ResponseModel<bool>("Ship Added Successfully");
        }
    }
}


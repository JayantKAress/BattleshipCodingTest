using BattleShipStateTracker.Common.Models;
using BattleShipStateTracker.Common.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Components.BattleShip
{
    public class CreateBoardCommand : IRequest<ResponseModel<bool>> { }

    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, ResponseModel<bool>>
    {
        private readonly IBattleShipService _battleShipService;
        public CreateBoardCommandHandler(IBattleShipService battleShipService)
        {
            _battleShipService = battleShipService;
        }

        public async Task<ResponseModel<bool>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            _battleShipService.CreateBoard();
            return new ResponseModel<bool>("Board of Size 10 X 10 Created Successfully");
        }
    }
}

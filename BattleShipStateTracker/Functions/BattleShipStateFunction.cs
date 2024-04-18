using BattleShipStateTracker.Components.BattleShip;
using BattleShipStateTracker.Components.BattleShip.Command;
using BattleShipStateTracker.Components.BattleShip.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BattleShipStateTracker.Functions
{
    public class BattleShipStateFunction : BaseFunction
    {
        public BattleShipStateFunction(IMediator mediator) : base(mediator) { }

        [FunctionName("CreateBoard")]
        public async Task<IActionResult> CreateBoard([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateBoard")] CreateBoardCommand createBoardCommand)
        {
            return await Ok(createBoardCommand);
        }

        [FunctionName("ShowBoard")]
        public async Task<HttpResponseMessage> ShowBoard([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ShowBoard")] ShowBoardQuery showBoardQuery)
        {
            var board = await Ok(showBoardQuery);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(board.Value.ToString(), Encoding.UTF8, "text/plain")
            };
            return response;
        }

        [FunctionName("AddShip")]
        public async Task<IActionResult> AddShip([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddShip")] AddShipCommand addShipCommand)
        {
            return await Ok(addShipCommand);
        }

        [FunctionName("FireShip")]
        public async Task<IActionResult> FireShip([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "FireShip")] FireShipCommand fireShipCommand)
        {
            return await Ok(fireShipCommand);
        }

        [FunctionName("RestartGame")]
        public async Task<IActionResult> RestartGame([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "RestartGame")] RestartGameQuery restartGameQuery)
        {
            return await Ok(restartGameQuery);
        }
    }
}

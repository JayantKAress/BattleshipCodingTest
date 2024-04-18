using BattleShipCodingTest.Shared.Exceptions;
using BattleShipStateTracker.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Functions
{
    public class BaseFunction
    {
        private readonly IMediator _mediator;
        public BaseFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ObjectResult> Ok<T>(T request) where T : class
        {
            try
            {
                var response = await _mediator.Send(request);
                return new OkObjectResult(response);
            }
            catch (Exception error)
            {

                var responseModel = new ResponseModel<object>() { Message = error?.Message };
                var badRequestModel = new BadRequestObjectResult(responseModel);
                switch (error)
                {
                    case BattleShipApiException:
                        // custom application error 
                        badRequestModel.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        // unhandled error
                        badRequestModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                return badRequestModel;
            }
        }
    }
}

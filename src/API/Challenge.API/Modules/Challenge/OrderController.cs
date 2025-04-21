using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dinet.Module.Challenge.Application.Contract;
using Dinet.Module.Challenge.Application.Orders.CreateOrder;
using Dinet.Module.Challenge.Application.Orders.GetOrder;
using Dinet.Module.Challenge.Application.Orders.UpdateOrder;
using Dinet.Module.Challenge.Domain.Orders;
using Dinet.Module.Challenge.Application.Orders.GetOrders;
using Dinet.Module.Challenge.Application.Orders.DeleteOrder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Dinet.Module.Challenge.Application;
using Microsoft.AspNetCore.Authorization;

namespace Challenge.API.Modules.Challenge
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController: ControllerBase
    {
        private readonly IChallengeModule challengeModule;
        public OrderController(IChallengeModule challengeModule) 
        {
            this.challengeModule = challengeModule;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(OperationResult<Guid>), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Create([FromBody] RequestOrderCreate request) 
        {
            var cmd = new CreateOrderCommand(request.Cliente, request.FechaCreacion);

            foreach (var item in request.Items)
            {
                cmd.OrdenDetails.Add(new(item.Producto, item.Cantidad, item.PrecioUnitario));
            }

            var result = await challengeModule.ExecuteCommandAsync(cmd);
            return this.Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{orderId}")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid orderId, [FromBody] RequestOrderUpdate request)
        {
            var cmd = new UpdateOrderCommand(orderId, request.Cliente, request.FechaCreacion);

            foreach (var item in request.Items)
            {
                cmd.OrdenDetails.Add(new(item.Id, item.Producto, item.Cantidad, item.PrecioUnitario));
            }

            var result = await challengeModule.ExecuteCommandAsync(cmd);
            if (result == null || !result.Success) 
            {
                return NotFound(new { success = false });
            }
            return this.Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("{orderId}")]
        [ProducesResponseType(typeof(OperationResult<GetOrderResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var result = await challengeModule.ExecuteQueryAsync(new GetOrderQuery(orderId));
            if (result.Data == null)
                return NotFound(new { success = false });
            return this.Ok(result);
        }
        [HttpDelete]
        [Route("{orderId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            var result = await challengeModule.ExecuteCommandAsync(new DeleteOrderCommand(orderId));
            return this.Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(OperationResult<GetOrdersResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagination([FromQuery]int startAt, int rowCount, string? cliente, DateTime? from, DateTime? to, string? fieldOrder ="FechaCreacion", bool orderDesc=true) 
        {
            var result = await challengeModule.ExecuteQueryAsync(new GetOrdersQuery(startAt, rowCount, cliente, from, to, fieldOrder, orderDesc));
            return Ok(result);
        }
    }
}

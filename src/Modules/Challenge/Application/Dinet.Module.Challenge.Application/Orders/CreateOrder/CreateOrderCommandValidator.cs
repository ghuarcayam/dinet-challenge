using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dinet.Module.Challenge.Application.Orders.CreateOrder.CreateOrderCommand;

namespace Dinet.Module.Challenge.Application.Orders.CreateOrder
{
    internal class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
            this.RuleFor(x=>x.Cliente).NotNull().NotEmpty();
            RuleFor(x => x.FechaCreacion).NotEmpty();
            this.RuleFor(x => x.OrdenDetails).NotNull().NotEmpty();
            RuleForEach(x => x.OrdenDetails).SetValidator(new CreateOrderItemCommandValidator());
        }

        private class CreateOrderItemCommandValidator : AbstractValidator<OrderItemCommand> 
        {
            public CreateOrderItemCommandValidator() 
            {
                RuleFor(x=>x.Producto).NotNull().NotEmpty();
                RuleFor(x => x.Cantidad).GreaterThan(0);
                RuleFor(x=>x.PrecioUnitario).GreaterThan(0);
            }
        }
    }
}

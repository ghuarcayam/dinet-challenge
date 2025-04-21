using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Domain.Orders;
using static Dinet.Module.Challenge.Application.Orders.UpdateOrder.UpdateOrderCommand;

namespace Dinet.Module.Challenge.Application.Orders.UpdateOrder
{
    internal class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            this.RuleFor(x => x.Cliente).NotNull().NotEmpty();
            this.RuleFor(x => x.OrdenDetails).NotNull().NotEmpty();
            RuleForEach(x => x.OrdenDetails).SetValidator(new UpdateOrderItemCommandValidator()); ;
        }

        private class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
        {
            public UpdateOrderItemCommandValidator()
            {
                RuleFor(x => x.Producto).NotNull().NotEmpty();
                RuleFor(x => x.Cantidad).GreaterThan(0);
                RuleFor(x => x.PrecioUnitario).GreaterThan(0);
            }
        }
    }
}

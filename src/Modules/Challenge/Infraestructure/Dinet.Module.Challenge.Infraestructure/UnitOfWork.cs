using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Infraestructure
{
    internal class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(
            DbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}

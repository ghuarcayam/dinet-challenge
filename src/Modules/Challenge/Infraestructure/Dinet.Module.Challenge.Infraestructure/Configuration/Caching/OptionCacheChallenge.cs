using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Infraestructure.Configuration.Caching
{
    internal class OptionCacheChallenge : IOptions<MemoryCacheOptions>
    {
        

        MemoryCacheOptions IOptions<MemoryCacheOptions>.Value => new MemoryCacheOptions() { };
    }
}

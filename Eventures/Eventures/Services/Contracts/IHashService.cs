using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Services.Contracts
{
    public interface IHashService
    {
        string GetHash(string rawData);
    }
}

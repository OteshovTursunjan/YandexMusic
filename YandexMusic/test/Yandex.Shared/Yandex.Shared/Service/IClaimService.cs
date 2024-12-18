using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex.Shared.Service;

public interface IClaimService
{
    string GetUserId();
    string GetClaim(string key);
}

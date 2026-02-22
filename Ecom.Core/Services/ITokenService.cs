using Ecom.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

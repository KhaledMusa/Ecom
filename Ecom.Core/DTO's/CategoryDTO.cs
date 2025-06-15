using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTO_s
{
    public record CategoryDTO
    ( string Name, string Description);
    public record UpdatecategoryDTO
   (int Id, string Name, string Description);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IJwtHelper
    {
        string? GetUserNameFromToken(string token);
        string? GetUserIdFromToken(string token);
    }
}

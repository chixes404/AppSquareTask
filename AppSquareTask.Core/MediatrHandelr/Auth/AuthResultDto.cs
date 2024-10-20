using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Core.MediatrHandelr.Auth
{
    public class AuthResultDto
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}

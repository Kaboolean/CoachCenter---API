using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Models.Auth
{
    public class TokenResult
    {
        public string? Token { get; set; }
        public int ExpiresIn { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string UserType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Models.Users
{
    public class DeleteUserModel
    {
        public Guid userId { get; set; }
        public string UserType { get; set; } = null!;
    }
}

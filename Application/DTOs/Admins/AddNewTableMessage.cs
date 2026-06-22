using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admins
{
    public class AddNewTableMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

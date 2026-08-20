using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class AuthResultMessge
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public string? Password  { get; set; }   
        public string? Email { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}

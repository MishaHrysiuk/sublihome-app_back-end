using System;
using System.Collections.Generic;
using System.Text;

namespace Sublihome.Application.Dto.Users
{
    public class NewUserPasswordDto
    {
        public int UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}

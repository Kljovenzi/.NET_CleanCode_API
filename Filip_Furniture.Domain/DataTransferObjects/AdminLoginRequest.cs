﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Domain.DataTransferObjects
{
    public class AdminLoginRequest
    {
        public string AdminLoginId { get; set; }
        public string Password { get; set; }
    }
}

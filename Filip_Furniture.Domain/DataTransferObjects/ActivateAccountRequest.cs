﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Domain.DataTransferObjects
{
    public class ActivateAccountRequest
    {
        public string EmailAddress { get; set; }
        public string ActivationCode { get; set; }
    }
}

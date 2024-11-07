﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class ChekIsExistProduct
    {
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }
    }
}

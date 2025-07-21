﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.DepartmentDto
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? code { get; set; }
        public string? Description { get; set; }
    }
}

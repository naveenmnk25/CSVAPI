﻿using System;
using System.Collections.Generic;

namespace CSVAPI.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventoryIncreaseModel
    {
        public long InventoryId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
        public long OperatorId { get; set; }
    }
}
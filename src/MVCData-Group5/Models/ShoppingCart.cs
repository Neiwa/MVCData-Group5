﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Models
{
    public class ShoppingCart : Dictionary<int, int>
    {
        public void Add(int id)
        {
            int value;
            TryGetValue(id, out value);
            this[id] = value++;
        }

        public new void Remove(int id)
        {
            int value;
            TryGetValue(id, out value);
            this[id] = --value < 0 ? 0 : value;
        }

        public int Total
        {
            get
            {
                return this.Sum(kvp => kvp.Value);
            }
        }
    }
}

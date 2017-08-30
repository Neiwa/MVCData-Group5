using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCData_Group5.Utilities
{
    public class ShoppingCart : Dictionary<int, int>
    {
        public void Add(int id)
        {
            int value;
            TryGetValue(id, out value);
            this[id] = ++value;
        }

        public void RemoveOne(int id)
        {
            int value;
            TryGetValue(id, out value);
            this[id] = --value < 0 ? 0 : value;
        }

        public void RemoveAll(int id)
        {
            this[id] = 0;
        }

        public void RemoveOneOfAll()
        {
            var keys = Keys.ToArray();
            foreach (var key in keys)
            {
                int count = this[key];
                this[key] = --count < 0 ? 0 : count;
            }
        }

        public int AmountItems
        {
            get
            {
                return this.Sum(kvp => kvp.Value);
            }
        }

        public string Serialize()
        {
            return this.Aggregate("SC1-", (s, kvp) => s + kvp.Key + "+" + kvp.Value + "-");
        }

        public static ShoppingCart Deserialize(string data)
        {
            if(data.Substring(0,3) != "SC1")
            {
                throw new FormatException("Data is not of a supported format.");
            }
            data = data.Substring(3);
            var cart = new ShoppingCart();

            foreach (var item in data.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] line = item.Split('+');
                cart.Add(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
            }
            return cart;
        }
    }
}

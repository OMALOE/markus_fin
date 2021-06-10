using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kursach.Shared.Models;

namespace kursach.Client.Models
{
    public class CartHelper
    {
        public List<OrderProduct> Cart { get; set; } = new List<OrderProduct>();
        public Action<OrderProduct> OnCartUpdate { get; set; }
        public Action OnCartClear { get; set; }
        public Action<int> OnCartDelete { get; set; }
        public void AddItemToCart(OrderProduct product)
        {
            int index = -1;
            foreach (var item in Cart)
            {
                if(product.Productid == item.Productid)
                {
                    index = Cart.IndexOf(item);
                    break;
                }
            }
            if (index >= 0)
            {
                Cart[index] = product;
            } else {
                Cart.Add(product);
            }
            Console.WriteLine("Added! 1");
            OnCartUpdate?.Invoke(product);
        }

        public void RemoveFromCart(int index)
        {
            Cart.RemoveAt(index);
            OnCartDelete?.Invoke(index);
        }
        public void ClearCart()
        {
            Cart.Clear();
            OnCartClear?.Invoke();
        }
    }
}

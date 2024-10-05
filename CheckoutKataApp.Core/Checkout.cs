using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKataApp.Core
{
    public class Checkout
    {
        private Dictionary<string, int> _itemCounts = [];
        private int _totalPrice = 0;

        public object? GetTotalPrice()
        {
            return _totalPrice;
        }

        public void Scan(string item)
        {
            if (!_itemCounts.ContainsKey(item))
            {

                _itemCounts[item] = 0; 
            }
            
            _itemCounts[item]++;
            CalculatePrice();
        }

        private void CalculatePrice()
        {
            // Reset total price to 0 before recalculating
            _totalPrice = 0;

            // Iterate through all scanned items (A, B, C, D)
            foreach (var item in _itemCounts)
            {
                string itemKey = item.Key;
                int itemCount = item.Value;

                if (itemKey == "A")
                {
                    // Apply the special offer "3 for 130" and remaining items at 50 each
                    int specialOfferGroups = itemCount / 3;    // How many groups of 3 "A"s can be used?
                    int remainingItems = itemCount % 3;        // How many leftover items to charge at the normal price?

                    _totalPrice += specialOfferGroups * 130 + remainingItems * 50;

                }
                else if (itemKey == "B")
                {
                    // Apply the special offer "2 for 45" and remaining items at 30 each
                    int specialOfferGroups = itemCount / 2;    // How many groups of 2 "B"s can be used?
                    int remainingItems = itemCount % 2;        // How many leftover items to charge at the normal price?

                    _totalPrice += specialOfferGroups * 45 + remainingItems * 30;
                }
                else if (itemKey == "C") 
                {
                    // No special offer for "C", just charge 20 for each
                    _totalPrice += itemCount * 20;
                }
                else if (itemKey == "D")
                {
                    // No special offer for "D", just charge 15 for each
                    _totalPrice += itemCount * 15;
                }
            }
        }
    }
}

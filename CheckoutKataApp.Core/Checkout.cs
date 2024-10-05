using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKataApp.Core
{
    public class Checkout
    {
        private Dictionary<string, (int UnitPrice, int OfferQuantity, int OfferPrice)> _pricingRules;
        private Dictionary<string, int> _itemCounts = [];
        private int _totalPrice = 0;

        public Checkout(Dictionary<string, (int UnitPrice, int OfferQuantity, int OfferPrice)> pricingRules)
        {
                _pricingRules = pricingRules;
        }

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

                //Get pricing rule for the item
                var pricingRule = _pricingRules[itemKey];
                var unitPrice = pricingRule.UnitPrice;
                var offerQuantity = pricingRule.OfferQuantity;
                var offerPrice = pricingRule.OfferPrice;

                if (offerQuantity > 0 && itemCount >= 0)
                {
                    _totalPrice += (itemCount / offerQuantity) * offerPrice + (itemCount % offerQuantity) * unitPrice;
                }
                else
                {
                    _totalPrice += itemCount * unitPrice;
                }
            }
        }
    }
}

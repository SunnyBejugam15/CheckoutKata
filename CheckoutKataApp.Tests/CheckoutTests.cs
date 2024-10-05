using CheckoutKataApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKataApp.Tests
{
    public class CheckoutTests
    {
        private Dictionary<string, (int UnitPrice, int OfferQuantity, int OfferPrice)> _pricingRules;

        [SetUp]
        public void Setup()
        {
            // Define pricing rules for each item
            _pricingRules = new Dictionary<string, (int UnitPrice, int OfferQuantity, int OfferPrice)>
            {
                {"A", (50, 3, 130)},  // "A" costs 50, offer: 3 for 130
                {"B", (30, 2, 45)},   // "B" costs 30, offer: 2 for 45
                {"C", (20, 0, 0)},    // "C" costs 20, no special offer
                {"D", (15, 0, 0)}     // "D" costs 15, no special offer
            };
        }

        [Test]
        public void Scan_SingleItem_ReturnsTheCorrectPrice()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void Scan_MultipleItesmsWithOutOffer_ReturnsTheCorrectTotalPrice()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(80));
        }

        [Test]
        public void Scan_MultupleItemsWithOffer_ReturnsTheCorrectTotalPrice()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(130));
        }
    }
}

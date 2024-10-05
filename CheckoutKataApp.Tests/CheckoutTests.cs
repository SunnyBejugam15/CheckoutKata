using CheckoutKataApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CheckoutKataApp.Tests
{
    [TestFixture]
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

     
        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        public void Scan_SingleItemA_ReturnsTheCorrectPrice(string item, int expectedTotal)
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan(item);
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedTotal));
        }


      
        [TestCase(new[] { "A", "B" }, 80)]  // A + B = 50 + 30 = 80
        [TestCase(new[] { "C", "D" }, 35)]  // C + D = 20 + 15 = 35
        public void Scan_MultipleItemsWithoutOffer_ReturnsCorrectTotal(string[] items, int expectedTotal)
        {
            var checkout = new Checkout(_pricingRules);
            foreach (var item in items)
            {
                checkout.Scan(item);
            }
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedTotal));
        }

        
        [TestCase(new[] { "A", "A", "A" }, 130)]  // 3 A's for 130
        [TestCase(new[] { "A", "A", "A", "A" }, 180)]  // 3 A's for 130 + 1 A for 50 = 180
        public void Scan_ApplySpecialOfferForA_ReturnsCorrectTotal(string[] items, int expectedTotal)
        {
            var checkout = new Checkout(_pricingRules);
            foreach (var item in items)
            {
                checkout.Scan(item);
            }
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedTotal));
        }

        [TestCase(new[] { "B", "B" }, 45)]  // 2 B's for 45
        [TestCase(new[] { "B", "B", "B" }, 75)]  // 2 B's for 45 + 1 B for 30 = 75
        public void Scan_ApplySpecialOfferForB_ReturnsCorrectTotal(string[] items, int expectedTotal)
        {
            var checkout = new Checkout(_pricingRules);
            foreach (var item in items)
            {
                checkout.Scan(item);
            }
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedTotal));
        }

       
        [TestCase(new[] { "A", "A", "A", "B", "B", "C", "D" }, 210)]  // Mixed items: 130 + 45 + 20 + 15 = 210
        public void Scan_MixedItems_ReturnsCorrectTotal(string[] items, int expectedTotal)
        {
            var checkout = new Checkout(_pricingRules);
            foreach (var item in items)
            {
                checkout.Scan(item);
            }
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(expectedTotal));
        }
    }
}

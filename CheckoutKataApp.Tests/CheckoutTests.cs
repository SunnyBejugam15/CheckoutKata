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
        public void Scan_SingleItemA_ReturnsTheCorrectPrice()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void Scan_SingleItemB_ReturnsTheCorrectPrice()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("B");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(30));
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

        [Test]  
        public void Scan_4As_ApplySpecialOfferAndSingleItem_ReturnsTheCorrectTotalPrice()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(180));
        }

        [Test]
        public void Scan_TwoBs_ApplySpecialOffer_ReturnsCorrectTotal()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("B");
            checkout.Scan("B");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(45));
        }

        [Test]
        public void Scan_MixedItems_ApplyOffers_ReturnsCorrectTotal()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Scan("D");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(210));
        }

        [Test]
        public void Scan_OneOfEachItem_NoOffers_ReturnsCorrectTotal()
        {
            var checkout = new Checkout(_pricingRules);
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Scan("D");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(115));
        }
    }
}

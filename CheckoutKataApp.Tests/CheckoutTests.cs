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
        [Test]
        public void Scan_SingleItem_ReturnsTheCorrectPrice()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void Scan_MultipleItesmsWithOutOffer_ReturnsTheCorrectTotalPrice()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(80));
        }

        [Test]
        public void Scan_MultupleItemsWithOffer_ReturnsTheCorrectTotalPrice()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(130));
        }
    }
}

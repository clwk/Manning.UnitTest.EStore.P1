using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using System.Collections.Generic;
using System;
using Xunit;

namespace ShoppingCartService.Tests
{
    public class ShippingCalculatorTests
    {
        [Fact]
        public void Calculate_same_city_shipping_Standard_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = CreateCart();

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(1.0, cost);
        }

        [Fact]
        public void Calculate_other_city_shipping_Standard_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = CreateCartWithAddress(CreateAddress("Stockholm"));

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(2.0, cost);
        }

        [Fact]
        public void Calculate_international_shipping_Standard_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = CreateCartWithAddress(CreateAddress("Stockholm", "Norway"));

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(15.0, cost);
        }

        #region expedited

        [Fact]
        public void Calculate_same_city_shipping_Expedited_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            var cart = CreateCartWithShippingMethod(ShippingMethod.Expedited);

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(1.2, cost);
        }

        [Fact]
        public void Calculate_other_city_shipping_Expedited_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            var cart = CreateCart(CreateAddress("Helsinki"), ShippingMethod.Expedited);

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(2.4, cost);
        }

        [Fact]
        public void Calculate_international_shipping_Expedited_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            var cart = CreateCart(CreateAddress("Oslo", "Norway"), ShippingMethod.Expedited);

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(18.0, cost);
        }

        #endregion

        #region builders
        private List<Item> CreateItems(double price = 1, string name = "Trams", uint quantity = 1)
        {
            return new List<Item>()
            {
                new Item()
                {
                    Price = price,
                    ProductName=name,
                    Quantity=quantity,
                    ProductId = $"Id{name}"
                }
            };
        }

        private Cart CreateCart(
            List<Item> items,
            Address address,
            string customerId = "id1",
            CustomerType type = CustomerType.Standard,
            ShippingMethod shippingMethod = ShippingMethod.Standard)
        {
            return new Cart()
            {
                CustomerId = customerId,
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Houston", Country = "USA" },
                ShippingMethod = ShippingMethod.Standard
            };
        }

        private Cart CreateCartWithAddress(Address address)
        {
            var cart = CreateCart();
            cart.ShippingAddress = address;
            return cart;
        }

        private Cart CreateCartWithShippingMethod(ShippingMethod method = ShippingMethod.Standard)
        {
            var cart = CreateCart();
            cart.ShippingMethod = method;
            return cart;
        }

        private Cart CreateCart(Address address = null, ShippingMethod method = ShippingMethod.Standard) =>
            new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = CreateItems(),
                ShippingAddress = address ??= CreateAddress(),
                ShippingMethod = method
            };

        private List<Item> CreateItems() =>
            new List<Item>()
            {
                new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 }
            };

        private Address CreateAddress(string city = "Dallas", string country = "USA", string street = "1234 left lane.") =>
            new Address()
            {
                City = city,
                Country = country,
                Street = street
            };

        #endregion
    }
}

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
        [Theory]
        [InlineData("Dallas", "USA", 1.0)]
        [InlineData("Stockholm", "USA", 2.0)]
        [InlineData("Stockholm", "Norway", 15.0)]
        public void Calculate_same_city_shipping_Standard_method(string city, string country, double expectedCost)
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = CreateCartWithAddress(CreateAddress(city, country));

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(expectedCost, cost);
        }

        #region expedited

        [Theory]
        [InlineData("Dallas", "USA", 1.2)]
        [InlineData("Helsinki", "USA", 2.4)]
        [InlineData("Oslo", "Norway", 18.0)]
        public void Calculate_same_city_shipping_Expedited_method(string city, string country, double expectedCost)
        {
            ShippingCalculator calculator = new ShippingCalculator();
            var cart = CreateCart(CreateAddress(city, country), ShippingMethod.Expedited);

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(expectedCost, cost);
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

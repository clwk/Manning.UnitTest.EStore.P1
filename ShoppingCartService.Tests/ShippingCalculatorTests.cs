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
            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Dallas", Country="USA" },
                ShippingMethod = ShippingMethod.Standard
            };

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(1.0, cost);
        }

        [Fact]
        public void Calculate_other_city_shipping_Standard_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Houston", Country = "USA" },
                ShippingMethod = ShippingMethod.Standard
            };

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(2.0, cost);
        }

        [Fact]
        public void Calculate_international_shipping_Standard_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Oslo", Country = "Norway" },
                ShippingMethod = ShippingMethod.Standard
            };

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(15.0, cost);
        }

        #region expedited

        [Fact]
        public void Calculate_same_city_shipping_Expedited_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Dallas", Country = "USA" },
                ShippingMethod = ShippingMethod.Expedited
            };

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(1.2, cost);
        }

        [Fact]
        public void Calculate_other_city_shipping_Expedited_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Houston", Country = "USA" },
                ShippingMethod = ShippingMethod.Expedited
            };

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(2.4, cost);
        }

        [Fact]
        public void Calculate_international_shipping_Expedited_method()
        {
            ShippingCalculator calculator = new ShippingCalculator();
            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Oslo", Country = "Norway" },
                ShippingMethod = ShippingMethod.Expedited
            };

            var cost = calculator.CalculateShippingCost(cart);

            Assert.Equal(18.0, cost);
        }

        #endregion
    }
}

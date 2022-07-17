using AutoMapper;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Mapping;
using ShoppingCartService.Models;
using System.Collections.Generic;
using Xunit;

namespace ShoppingCartService.Tests
{
    public class CheckOutEngineTests
    {
        [Fact]
        public void Checkout_calculateTotals_correct_total()
        {
            // Arrange
            ShippingCalculator shippingCalculator = new ShippingCalculator();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            var checkoutEngine = new CheckOutEngine(shippingCalculator, mapper);

            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Dallas", Country = "USA" },
                ShippingMethod = ShippingMethod.Standard
            };

            // Act
            var checkoutDto = checkoutEngine.CalculateTotals(cart);

            // Assert
            Assert.Equal(2.0, checkoutDto.Total);
        }

        [Fact]
        public void Checkout_calculate_checkoutDto_Cart_mapping_matches()
        {
            // Arrange
            ShippingCalculator shippingCalculator = new ShippingCalculator();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            var checkoutEngine = new CheckOutEngine(shippingCalculator, mapper);

            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Standard,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Dallas", Country = "USA" },
                ShippingMethod = ShippingMethod.Standard
            };

            // Act
            var checkoutDto = checkoutEngine.CalculateTotals(cart);

            // Assert
            Assert.Equal(cart.Id, checkoutDto.ShoppingCart.Id);
            Assert.Equal(cart.CustomerId, checkoutDto.ShoppingCart.CustomerId);
            Assert.Equal(cart.Id, checkoutDto.ShoppingCart.Id);
            Assert.Equal(cart.ShippingMethod, checkoutDto.ShoppingCart.ShippingMethod);
            Assert.Equal(cart.ShippingAddress, checkoutDto.ShoppingCart.ShippingAddress);
        }

        [Fact]
        public void Checkout_calculate_checkoutDto_discount_correct()
        {
            // Arrange
            ShippingCalculator shippingCalculator = new ShippingCalculator();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            var checkoutEngine = new CheckOutEngine(shippingCalculator, mapper);

            Cart cart = new Cart()
            {
                CustomerId = "id1",
                CustomerType = CustomerType.Premium,
                Items = new List<Item>() { new Item() { Price = 1, ProductName = "Bengt", Quantity = 1 } },
                ShippingAddress = new Address() { City = "Dallas", Country = "USA" },
                ShippingMethod = ShippingMethod.Standard
            };

            // Act
            var checkoutDto = checkoutEngine.CalculateTotals(cart);

            // Assert
            Assert.Equal(10.0, checkoutDto.CustomerDiscount);
           
        }
    }
}

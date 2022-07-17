using ShoppingCartService.Models;
using Xunit;
using ShoppingCartService.BusinessLogic.Validation;

namespace ShoppingCartService.Tests
{
    public class AddressValidatorTests
    {
        private Address FullAddress { get; }

        public AddressValidatorTests()
        {
            FullAddress = new Address()
            {
                City = "Oslo",
                Country = "Norway",
                Street = "King's Street"
            };
        }
        [Fact]
        public void Address_null_isinvalid()
        {
            Address address = null;
            var validator = new AddressValidator();

            var isValid = validator.IsValid(address);

            Assert.False(isValid);
        }

        [Fact]
        public void Address_city_empty_isinvalid()
        {
            var addressCityEmpty = FullAddress with { City = "" };
            var validator = new AddressValidator();

            var isValid = validator.IsValid(addressCityEmpty);

            Assert.False(isValid);
        }

        [Fact]
        public void Address_country_empty_isinvalid()
        {
            var addressCityEmpty = FullAddress with { Country="" };
            var validator = new AddressValidator();

            var isValid = validator.IsValid(addressCityEmpty);

            Assert.False(isValid);
        }

        [Fact]
        public void Address_street_empty_isinvalid()
        {
            var addressCityEmpty = FullAddress with { Street="" };
            var validator = new AddressValidator();

            var isValid = validator.IsValid(addressCityEmpty);

            Assert.False(isValid);
        }

        [Fact]
        public void Address_3_fields_entered_is_valid()
        {
            var address = new Address()
            {
                City = "Oslo",
                Country = "Norway",
                Street = "King's Street"
            };
            var validator = new AddressValidator();

            var isValid = validator.IsValid(address);

            Assert.True(isValid);
        }
    }
}

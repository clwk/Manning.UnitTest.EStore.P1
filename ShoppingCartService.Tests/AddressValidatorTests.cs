using ShoppingCartService.Models;
using Xunit;
using ShoppingCartService.BusinessLogic.Validation;
using System.Collections;
using System.Collections.Generic;

namespace ShoppingCartService.Tests
{
    public class AddressValidatorTests
    {
        private static Address FullAddress { get; }

        static AddressValidatorTests()
        {
            FullAddress = new Address()
            {
                City = "Oslo",
                Country = "Norway",
                Street = "King's Street"
            };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Address_null_or_any_empty_field_isinvalid(Address address)
        {
            var validator = new AddressValidator();

            var isValid = validator.IsValid(address);

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

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {null },
                new object[] {FullAddress with { City = "" } },
                new object[] {FullAddress with { Country = "" } },
                new object[] {FullAddress with { Street = "" } },
            };
    }
}

using Domain.Enums;
using Domain.Extensions;
using System.Linq;
using Xunit;

namespace Domain.UnitTests.Extensions
{
    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData(new int[] { 0, 1, 2, 3 }, new string[] { "Unknown", "Sedan", "Coupe", "Sports" })]
        [InlineData(new int[] { 7, 8, 9, 4 }, new string[] { "SUV", "Minivan", "Pickup", "Wagon" })]
        public void ToEnumValueArray_ShouldConvertListOfStringsIntoArrayOfBodyStyleValues(int[] expected, string[] nameArray)
        {
            //arrange
            var nameList = nameArray.ToList();

            //act
            var actual = nameList.ToEnumValueArray<BodyStyle>();

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, new string[] { "Manual", "Automatic" })]
        [InlineData(new int[] { 3, 4 }, new string[] { "SemiAutomatic", "CVT" })]
        public void ToEnumValueArray_ShouldConvertListOfStringsIntoArrayOfTransmissionValues(int[] expected, string[] nameArray)
        {
            //arrange
            var nameList = nameArray.ToList();

            //act
            var actual = nameList.ToEnumValueArray<Transmission>();

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(BodyStyle.Unknown, "Unknown")]
        [InlineData(BodyStyle.Sedan, "Sedan")]
        [InlineData(BodyStyle.Coupe, "Coupe")]
        [InlineData(BodyStyle.Sports, "Sports")]
        public void ToEnum_ShouldConvertStringToBodyStyle(BodyStyle expected, string name)
        {
            //act
            var actual = name.ToEnum<BodyStyle>();

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(Drivetrain.Unknown, "Unknown")]
        [InlineData(Drivetrain.AllWheel, "AllWheel")]
        [InlineData(Drivetrain.RearWheel, "RearWheel")]
        [InlineData(Drivetrain.FrontWheel, "FrontWheel")]
        public void ToEnum_ShouldConvertStringToDrivetrain(Drivetrain expected, string name)
        {
            //act
            var actual = name.ToEnum<Drivetrain>();

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(Transmission.Unknown, "Unknown")]
        [InlineData(Transmission.Manual, "Manual")]
        [InlineData(Transmission.Automatic, "Automatic")]
        [InlineData(Transmission.SemiAutomatic, "SemiAutomatic")]
        [InlineData(Transmission.CVT, "CVT")]
        public void ToEnum_ShouldConvertStringToTransmission(Transmission expected, string name)
        {
            //act
            var actual = name.ToEnum<Transmission>();

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(SellerType.Dealer, "Dealer")]
        [InlineData(SellerType.Individual, "Individual")]
        public void ToEnum_ShouldConvertStringToSellerType(SellerType expected, string name)
        {
            //act
            var actual = name.ToEnum<SellerType>();

            //assert
            Assert.Equal(expected, actual);
        }
    }
}

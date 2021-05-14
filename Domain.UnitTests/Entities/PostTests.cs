using Domain.Entities;
using Domain.Enums;
using Xunit;

namespace Domain.UnitTests.Entities
{
    public class PostTests
    {
        [Theory]
        [InlineData(Drivetrain.AllWheel, "AllWheel")]
        [InlineData(Drivetrain.FrontWheel, "FrontWheel")]
        public void SetDrivetrain_ShouldSetFromString(Drivetrain expected, string name)
        {
            //arrange
            var post = new Post();

            //act
            post.SetDrivetrain(name);

            //assert
            Assert.Equal(expected, post.Drivetrain);
        }

        [Theory]
        [InlineData(Transmission.SemiAutomatic, "SemiAutomatic")]
        [InlineData(Transmission.Automatic, "Automatic")]
        public void SetTransmission_ShouldSetFromString(Transmission expected, string name)
        {
            //arrange
            var post = new Post();

            //act
            post.SetTransmission(name);

            //assert
            Assert.Equal(expected, post.Transmission);
        }

        [Theory]
        [InlineData(BodyStyle.Coupe, "Coupe")]
        [InlineData(BodyStyle.Buggy, "Buggy")]
        public void SetBodyStyle_ShouldSetFromString(BodyStyle expected, string name)
        {
            //arrange
            var post = new Post();

            //act
            post.SetBodyStyle(name);

            //assert
            Assert.Equal(expected, post.Body);
        }
    }
}

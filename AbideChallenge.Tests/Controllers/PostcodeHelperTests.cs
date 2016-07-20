using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbideChallenge.Data.Concrete;
using AbideChallenge.Domain;

namespace AbideChallenge.Tests.Controllers
{
    [TestClass]
    public class PostcodeHelperTests
    {
        [TestMethod]
        public void PostcodeEmpty()
        {
            // Arrange
            PostcodeHelper postcodeHelper = new PostcodeHelper();

            // Act
            Region result = postcodeHelper.GetRegion("");

            // Assert
            Assert.AreEqual(Region.Unknown, result);
        }

        [TestMethod]
        public void PostcodeNoSpace()
        {
            // Arrange
            PostcodeHelper postcodeHelper = new PostcodeHelper();

            // Act
            Region result = postcodeHelper.GetRegion("W11AA");

            // Assert
            Assert.AreEqual(Region.Unknown, result);
        }

        [TestMethod]
        public void PostcodeFirstCharIsNumber()
        {
            // Arrange
            PostcodeHelper postcodeHelper = new PostcodeHelper();

            // Act
            Region result = postcodeHelper.GetRegion("11 1AA");

            // Assert
            Assert.AreEqual(Region.Unknown, result);
        }


        [TestMethod]
        public void PostcodeOneCharacterArea()
        {
            // Arrange
            PostcodeHelper postcodeHelper = new PostcodeHelper();

            // Act
            Region result = postcodeHelper.GetRegion("W1 1AA");

            // Assert
            Assert.AreEqual(Region.CentralLondon, result);
        }

        [TestMethod]
        public void PostcodeTwoCharacterArea()
        {
            // Arrange
            PostcodeHelper postcodeHelper = new PostcodeHelper();

            // Act
            Region result = postcodeHelper.GetRegion("BA1 5HT");

            // Assert
            Assert.AreEqual(Region.SouthWest, result);
        }

    }
}

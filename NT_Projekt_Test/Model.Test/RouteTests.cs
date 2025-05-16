using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT_Projekt.Model;
using System;

namespace NT_Projekt_Test
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            string expectedStart = "Aalborg";
            string expectedEnd = "Hjørring";
            TimeSpan expectedDuration = new TimeSpan(1, 15, 0); // 1 time og 15 min
            double expectedEnergyUsage = 32.5;
            double expectedDistance = 68.0;
            string expectedRouteID = "R001";

            // Act
            Route route = new Route(
                expectedStart,
                expectedEnd,
                expectedDuration,
                expectedEnergyUsage,
                expectedDistance,
                expectedRouteID
            );

            // Assert
            Assert.AreEqual(expectedStart, route.StartPoint);
            Assert.AreEqual(expectedEnd, route.EndPoint);
            Assert.AreEqual(expectedDuration, route.EstimatedDuration);
            Assert.AreEqual(expectedEnergyUsage, route.EstimatedEnergyUsage);
            Assert.AreEqual(expectedDistance, route.Distance);
            Assert.AreEqual(expectedRouteID, route.RouteID);
        }

        [TestMethod]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            Route route = new Route(
                "Aalborg",
                "Skagen",
                new TimeSpan(2, 0, 0),
                45.0,
                120.0,
                "R100"
            );

            string expected = "Aalborg;Skagen;02:00:00;45;120;R100"; // Forventet format

            // Act
            string result = route.ToString();

            // Assert
            Assert.AreEqual(expected, result); // Tjek at ToString returnerer korrekt format
        }
    }
}
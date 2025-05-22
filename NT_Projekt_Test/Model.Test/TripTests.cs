using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT_Projekt.Model;
using System;

namespace NT_Projekt_Test
{
    [TestClass]
    public class TripTests
    {
        [TestMethod]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            DateTime expectedDate = new DateTime(2024, 5, 1, 8, 30, 0);
            double expectedEnergy = 34;
            TimeSpan expectedDuration = TimeSpan.FromMinutes(90);
            string expectedRouteID = "R300";
            string expectedLicensePlate = "XY12345";
            string expectedUserID = "U789";
            string expectedTripID = "T900";
            string expectedComment = "This is a comment";

            // Act
            Trip trip = new Trip(expectedDate, expectedEnergy, expectedDuration,
                                 expectedRouteID, expectedLicensePlate, expectedUserID, expectedTripID, expectedComment);

            // Assert
            Assert.AreEqual(expectedDate, trip.DateTime);
            Assert.AreEqual(expectedEnergy, trip.EnergyUsage);
            Assert.AreEqual(expectedDuration, trip.Duration);
            Assert.AreEqual(expectedRouteID, trip.RouteID);
            Assert.AreEqual(expectedLicensePlate, trip.LicensePlate);
            Assert.AreEqual(expectedUserID, trip.UserID);
            Assert.AreEqual(expectedTripID, trip.TripID);
        }

        [TestMethod]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            Trip trip = new Trip(
                new DateTime(2024, 5, 1, 8, 30, 0),
                34,
                TimeSpan.FromMinutes(90),
                "R300",
                "XY12345",
                "U789",
                "T900",
                "This is a comment"
                );
            string expected = string.Join(";",
                trip.DateTime.ToString(),
                trip.EnergyUsage.ToString(),
                trip.Duration.ToString(),
                trip.RouteID,
                trip.LicensePlate,
                trip.UserID,
                trip.TripID,
                trip.Comment
            );
            // Act
            string result = trip.ToString();

            // Assert
            Assert.AreEqual(expected, result); // Tjek at ToString giver korrekt format
        }

        [TestMethod]
        public void FromString_ParsesCorrectly()
        {
            // Arrange
            string line = "01-05-2024 08:30:00;34;01:30:00;R300;XY12345;U789;T900;This is a comment";

            // Act
            Trip trip = Trip.FromString(line);

            // Assert
            Assert.AreEqual(new DateTime(2024, 5, 1, 8, 30, 0), trip.DateTime); // Tjek dato
            Assert.AreEqual(34, trip.EnergyUsage);                              // Tjek energiforbrug
            Assert.AreEqual(TimeSpan.FromMinutes(90), trip.Duration);           // Tjek varighed
            Assert.AreEqual("R300", trip.RouteID);                              // Tjek rute
            Assert.AreEqual("XY12345", trip.LicensePlate);                      // Tjek nummerplade
            Assert.AreEqual("U789", trip.UserID);                               // Tjek bruger-ID
            Assert.AreEqual("T900", trip.TripID);                               // Tjek TripID
            Assert.AreEqual("This is a comment", trip.Comment);                 // Tjek kommentar
        }

        [TestMethod]
        public void ToString_ThenFromString_ReturnsEquivalentTrip()
        {
            // Arrange
            Trip original = new Trip(
                new DateTime(2024, 5, 1, 8, 30, 0),
                34,
                TimeSpan.FromMinutes(90),
                "R300",
                "XY12345",
                "U789",
                "T900",
                "This is a comment"
            );

            // Act
            string line = original.ToString();         // Gem som tekst
            Trip copy = Trip.FromString(line);         // Læs teksten tilbage

            // Assert – Sammenlign alle felter
            Assert.AreEqual(original.DateTime, copy.DateTime);
            Assert.AreEqual(original.EnergyUsage, copy.EnergyUsage);
            Assert.AreEqual(original.Duration, copy.Duration);
            Assert.AreEqual(original.RouteID, copy.RouteID);
            Assert.AreEqual(original.LicensePlate, copy.LicensePlate);
            Assert.AreEqual(original.UserID, copy.UserID);
            Assert.AreEqual(original.TripID, copy.TripID);
        }
    }
}

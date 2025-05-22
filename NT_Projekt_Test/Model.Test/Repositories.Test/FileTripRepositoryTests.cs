using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT_Projekt.Model;
using NT_Projekt.Model.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace NT_Projekt_Test
{
    [TestClass]
    public class FileTripRepositoryTests
    {
        private string _testFilePath;

        [TestInitialize]
        public void Setup()
        {
            // Opret midlertidig testfil
            _testFilePath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Slet testfil efter test
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void AddTrip_ThenGetAllTrips_ShouldContainAddedTrip()
        {
            // Arrange
            FileTripRepository repo = new FileTripRepository(_testFilePath);
            Trip newTrip = new Trip(
                new DateTime(2024, 5, 1, 8, 30, 0),
                34.5,
                new TimeSpan(1, 30, 0),
                "R300",
                "XY12345",
                "U789",
                "T900",
                "This is a comment"
            );

            // Act
            repo.AddTrip(newTrip);
            List<Trip> result = repo.GetAllTrips();

            // Assert
            Assert.AreEqual(1, result.Count); // Tjekker at én Trip blev gemt
            Assert.AreEqual("T900", result[0].TripID); // Tjek at én tur blev tilføjet
            Assert.AreEqual("XY12345", result[0].LicensePlate); // Tjek Trip ID
            Assert.AreEqual("R300", result[0].RouteID); // Tjek nummerplade
            Assert.AreEqual("U789", result[0].UserID); // Tjek rute
            Assert.AreEqual("This is a comment", result[0].Comment); // Tjek kommentar
        }

        [TestMethod]
        public void GetAllTrips_FileEmpty_ReturnsEmptyList()
        {
            // Arrange
            FileTripRepository repo = new FileTripRepository(_testFilePath);

            // Act
            List<Trip> result = repo.GetAllTrips();

            // Assert
            Assert.AreEqual(0, result.Count); // Forvent tom liste
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT_Projekt.Model;
using NT_Projekt.Model.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace NT_Projekt_Test
{
    [TestClass]
    public class FileBusRepositoryTests
    {
        private string _testFilePath;

        [TestInitialize]
        public void Setup()
        {
            // Opret midlertidig fil til test
            _testFilePath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Slet testfil efter hver test
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void AddBus_ThenGetAllBuses_ShouldContainAddedBus()
        {
            // Arrange
            FileBusRepository repo = new FileBusRepository(_testFilePath);
            Bus newBus = new Bus("Volvo", "8500", "AN55555", "Diesel");

            // Act
            repo.AddBus(newBus);
            List<Bus> result = repo.GetAllBuses();

            // Assert
            Assert.AreEqual(1, result.Count); // Der bør være præcis én bus
            Assert.AreEqual("Volvo", result[0].Brand); // Tjek brand
            Assert.AreEqual("8500", result[0].Model); // Tjek model
            Assert.AreEqual("AN55555", result[0].LicensePlate); // Tjek licensePlate
            Assert.AreEqual("Diesel", result[0].EnergyType); // Tjek energyType
        }

        [TestMethod]
        public void GetAllBuses_FileEmpty_ReturnsEmptyList()
        {
            // Arrange
            FileBusRepository repo = new FileBusRepository(_testFilePath);

            // Act
            List<Bus> result = repo.GetAllBuses();

            // Assert
            Assert.AreEqual(0, result.Count); // Der bør ikke være nogen busser
        }
    }
}
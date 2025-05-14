using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT_Projekt.Model;
using NT_Projekt.Model.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace NT_Projekt_Test
{
    [TestClass]
    public class FileRouteRepositoryTests
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
            // Slet testfil efter testen
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void AddRoute_ThenGetAllRoutes_ShouldContainAddedRoute()
        {
            // Arrange
            FileRouteRepository repo = new FileRouteRepository(_testFilePath);
            Route newRoute = new Route(
                "Aalborg",
                "Skagen",
                new TimeSpan(2, 15, 0),
                40.0,
                100.5,
                "R789"
            );

            // Act
            repo.AddRoute(newRoute);
            List<Route> result = repo.GetAllRoutes();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("R789", result[0].RouteID);
            Assert.AreEqual("Aalborg", result[0].StartPoint);
            Assert.AreEqual("Skagen", result[0].EndPoint);
            Assert.AreEqual(100.5, result[0].Distance);
        }

        [TestMethod]
        public void GetAllRoutes_FileEmpty_ReturnsEmptyList()
        {
            // Arrange
            FileRouteRepository repo = new FileRouteRepository(_testFilePath);

            // Act
            List<Route> result = repo.GetAllRoutes();

            // Assert
            Assert.AreEqual(0, result.Count); // Forvent tom liste
        }
    }
}
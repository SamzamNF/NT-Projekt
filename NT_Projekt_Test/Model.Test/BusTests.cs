using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model;

namespace NT_Projekt_Test
{
    [TestClass]
    public class BusTests
    {
        [TestMethod]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            string expectedBrand = "Volvo";
            string expectedModel = "7900 Electric";
            string expectedLicensePlate = "AB12345";
            string expectedEnergyType = "El";

            // Act
            Bus bus = new Bus(expectedBrand, expectedModel, expectedLicensePlate, expectedEnergyType);

            // Assert
            Assert.AreEqual(expectedBrand, bus.Brand); // Tjek brand
            Assert.AreEqual(expectedModel, bus.Model); // Tjek model
            Assert.AreEqual(expectedLicensePlate, bus.LicensePlate); // Tjek registreringsnummer
            Assert.AreEqual(expectedEnergyType, bus.EnergyType); // Tjek energitype
        }

        [TestMethod]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            Bus bus = new Bus("Mercedes", "Citaro", "CD67890", "Diesel");
            string expected = "Mercedes;Citaro;CD67890;Diesel"; // Forventet format

            // Act
            string result = bus.ToString();

            // Assert
            Assert.AreEqual(expected, result); // Tjek om ToString returnerer korrekt format
        }
    }
}
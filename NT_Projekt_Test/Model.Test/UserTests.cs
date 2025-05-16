using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT_Projekt.Model;

namespace NT_Projekt_Test
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            string expectedFirstName = "Lars";
            string expectedLastName = "Andersen";
            string expectedUserID = "U123";

            // Act
            User user = new User(expectedFirstName, expectedLastName, expectedUserID);

            // Assert
            Assert.AreEqual(expectedFirstName, user.FirstName); // Tjek fornavn
            Assert.AreEqual(expectedLastName, user.LastName); // Tjek efternavn
            Assert.AreEqual(expectedUserID, user.UserID); // Tjek bruger-ID
        }

        [TestMethod]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            User user = new User("Mette", "Jensen", "U456");
            string expected = "Mette;Jensen;U456"; // Forventer dette format fra ToString

            // Act
            string result = user.ToString();

            // Assert
            Assert.AreEqual(expected, result); // Tjek om ToString returnerer korrekt format
        }
    }
}
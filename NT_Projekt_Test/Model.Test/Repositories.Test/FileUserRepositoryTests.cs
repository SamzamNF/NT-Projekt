using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT_Projekt.Model;
using NT_Projekt.Model.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace NT_Projekt_Test
{
    [TestClass]
    public class FileUserRepositoryTests
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
            // Slet filen efter test
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void AddUser_ThenGetAllUsers_ShouldContainAddedUser()
        {
            // Arrange
            FileUserRepository repo = new FileUserRepository(_testFilePath);
            User user = new User("Sofie", "Hansen", "U007");

            // Act
            repo.AddUser(user);
            List<User> result = repo.GetAllUsers();

            // Assert
            Assert.AreEqual(1, result.Count); // Tjek at én bruger blev tilføjet
            Assert.AreEqual("U007", result[0].UserID); // Tjek bruger-ID
            Assert.AreEqual("Sofie", result[0].FirstName); // Tjek fornavn
            Assert.AreEqual("Hansen", result[0].LastName); // Tjek efternavn
        }

        [TestMethod]
        public void GetAllUsers_FileEmpty_ReturnsEmptyList()
        {
            // Arrange
            FileUserRepository repo = new FileUserRepository(_testFilePath);

            // Act
            List<User> result = repo.GetAllUsers();

            // Assert
            Assert.AreEqual(0, result.Count); // Forvent tom liste
        }
    }
}
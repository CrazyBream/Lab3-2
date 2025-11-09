using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services;
using Core.Entities;
using DAL;
using DAL.DataProviders;
using System.Collections.Generic;
using BLL.Exceptions;
using System.Linq;


namespace Tests
{
    [TestClass]
    public class ParticipantServiceTests
    {
        private ParticipantService _service;
        private ParticipantContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ParticipantContext(new JsonDataProvider());
            _service = new ParticipantService(_context);
        }

        [TestMethod]
        public void CountStudentsFromUkraineInThirdCourse_ReturnsCorrectCount()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678"),
                new Student("Петренко", 3, "AB654321", 90, "Україна", "ZB87654321"),
                new Student("Smith", 3, "AB111111", 75, "Польща", "ZB22222222")
            };

            // Act
            int result = _service.CountStudentsFromUkraineInThirdCourse(participants);

            // Assert
            Assert.AreEqual(2, result, "Очікується 2 студенти 3 курсу з України");
        }

        [TestMethod]
        [ExpectedException(typeof(ChessGameException))]
        public void PlayChessGame_LessThanTwoPlayers_ThrowsException()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Manager("Іван", "IT", 10)
            };

            // Act
            _service.PlayChessGame(participants);
        }

        [TestMethod]
        public void PlayChessGame_TwoPlayers_ReturnsResult()
        {
            // Arrange
            var participants = new List<Person>
            {
            new Manager("Іван", "IT", 10) { CanPlayChess = true },
            new Manager("Петро", "HR", 5) { CanPlayChess = true }
            };
            var service = new ParticipantService(new ParticipantContext(new JsonDataProvider()));

            // Act
            var result = service.PlayChessGame(participants);

            // Assert
            Assert.IsTrue(participants.Any(p => p.Name == result.Winner),
                "Переможець має бути одним із гравців");
            Assert.IsTrue(result.Moves >= 20 && result.Moves <= 60,
                "Кількість ходів повинна бути в межах 20–60");
        }


        [TestMethod]
        [ExpectedException(typeof(FileOperationException))]
        public void SaveParticipantsToFile_InvalidFileName_ThrowsException()
        {
            // Arrange
            var participants = new List<Person>();
            string filename = "invalid|name";

            // Act
            _service.SaveParticipantsToFile(participants, filename);
        }
    }
}

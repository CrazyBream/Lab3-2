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
        public void CountStudentsFromUkraineInThirdCourse_EmptyList_ReturnsZero()
        {
            // Arrange
            var emptyList = new List<Person>();

            // Act
            var result = _service.CountStudentsFromUkraineInThirdCourse(emptyList);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetStudentsFromUkraineInThirdCourse_ReturnsCorrectList()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678"),
                new Student("Петренко", 2, "AB654321", 90, "Україна", "ZB87654321"), // не 3 курс
                new Student("Smith", 3, "AB111111", 75, "Польща", "ZB22222222"), // не Україна
                new Manager("Менеджер", "IT", 5) // не студент
            };

            // Act
            var result = _service.GetStudentsFromUkraineInThirdCourse(participants);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Іваненко", result[0].LastName);
            Assert.AreEqual(3, result[0].Course);
            Assert.AreEqual("Україна", result[0].Country);
        }

        [TestMethod]
        public void GetChessPlayers_ReturnsOnlyChessPlayers()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678") { CanPlayChess = true },
                new Manager("Петро", "IT", 5) { CanPlayChess = false },
                new MсdonaldsWorker("Олена", "Касир", 100) { CanPlayChess = true },
                new Manager("Марія", "HR", 3) { CanPlayChess = false }
            };

            // Act
            var result = _service.GetChessPlayers(participants);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(p => p.CanPlayChess));
            Assert.IsTrue(result.Any(p => p.Name.Contains("Іваненко")));
            Assert.IsTrue(result.Any(p => p.Name.Contains("Олена")));
        }

        [TestMethod]
        public void GetChessPlayers_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Person>();

            // Act
            var result = _service.GetChessPlayers(emptyList);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ChessGameException))]
        public void PlayChessGame_LessThanTwoPlayers_ThrowsException()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Manager("Іван", "IT", 10) { CanPlayChess = true } // тільки один гравець
            };

            // Act
            _service.PlayChessGame(participants);
        }

        [TestMethod]
        [ExpectedException(typeof(ChessGameException))]
        public void PlayChessGame_NoChessPlayers_ThrowsException()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Manager("Іван", "IT", 10) { CanPlayChess = false },
                new Student("Петро", 3, "AB123456", 80, "Україна", "ZB12345678") { CanPlayChess = false }
            };

            // Act
            _service.PlayChessGame(participants);
        }

        [TestMethod]
        public void PlayChessGame_TwoPlayers_ReturnsValidResult()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Manager("Іван", "IT", 10) { CanPlayChess = true },
                new Manager("Петро", "HR", 5) { CanPlayChess = true }
            };

            // Act
            var result = _service.PlayChessGame(participants);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(participants.Any(p => p.Name == result.Winner),
                "Переможець має бути одним із гравців");
            Assert.IsTrue(result.Moves >= 20 && result.Moves <= 60,
                "Кількість ходів повинна бути в межах 20–60");
            Assert.IsNotNull(result.Player1);
            Assert.IsNotNull(result.Player2);
        }

        [TestMethod]
        public void PlayChessGame_MultiplePlayers_ReturnsValidResult()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Manager("Іван", "IT", 10) { CanPlayChess = true },
                new Manager("Петро", "HR", 5) { CanPlayChess = true },
                new Student("Олег", 3, "AB123456", 85, "Україна", "ZB12345678") { CanPlayChess = true },
                new MсdonaldsWorker("Марія", "Касир", 120) { CanPlayChess = true }
            };

            // Act
            var result = _service.PlayChessGame(participants);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(participants.Any(p => p.Name == result.Winner));
            Assert.IsTrue(result.Moves >= 20 && result.Moves <= 60);
        }

        [TestMethod]
        public void SetDataProvider_ValidJsonFormat_SetsProvider()
        {
            // Act
            _service.SetDataProvider("json");

            // Assert
            Assert.AreEqual(".json", _service.GetCurrentFileExtension());
        }

        [TestMethod]
        public void SetDataProvider_ValidXmlFormat_SetsProvider()
        {
            // Act
            _service.SetDataProvider("xml");

            // Assert
            Assert.AreEqual(".xml", _service.GetCurrentFileExtension());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void SetDataProvider_InvalidFormat_ThrowsException()
        {
            // Act
            _service.SetDataProvider("txt");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void SetDataProvider_EmptyFormat_ThrowsException()
        {
            // Act
            _service.SetDataProvider("");
        }

        [TestMethod]
        public void GetSupportedFormats_ReturnsCorrectList()
        {
            // Act
            var formats = _service.GetSupportedFormats();

            // Assert
            Assert.AreEqual(2, formats.Count);
            CollectionAssert.Contains(formats, "json");
            CollectionAssert.Contains(formats, "xml");
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

        [TestMethod]
        [ExpectedException(typeof(FileOperationException))]
        public void LoadParticipantsFromFile_InvalidFileName_ThrowsException()
        {
            // Arrange
            string filename = "invalid|name.json";

            // Act
            _service.LoadParticipantsFromFile(filename);
        }

        [TestMethod]
        public void ValidateStudent_ValidData_Passes()
        {
            // Arrange
            var student = new Student("Іваненко", 3, "AB123456", 85, "Україна", "ZB12345678");

            // Act & Assert - не повинно бути винятків
            _service.ValidateStudent(student);
        }

        [TestMethod]
        public void ValidateMcdonaldsWorker_ValidData_Passes()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "Касир", 100);

            // Act & Assert - не повинно бути винятків
            _service.ValidateMсdonaldsWorker(worker);
        }

        [TestMethod]
        public void ValidateManager_ValidData_Passes()
        {
            // Arrange
            var manager = new Manager("Петро", "IT", 5);

            // Act & Assert - не повинно бути винятків
            _service.ValidateManager(manager);
        }
    
    [TestMethod]
        public void PrepareBurger_ValidWorker_ReturnsBurger()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "Касир", 100);
            string[] ingredients = { "булочка", "котлета", "сир", "салат" };

            // Act
            var result = _service.PrepareBurger(worker, ingredients);

            // Assert
            Assert.IsTrue(result.Contains("Приготовано бургер"));
            Assert.IsTrue(result.Contains(string.Join(", ", ingredients)));
        }

        [TestMethod]
        public void PrepareSpecialBurger_ValidWorker_ReturnsSpecialBurger()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Олена", "Кухар", 200);

            // Act
            var result = _service.PrepareSpecialBurger(worker);

            // Assert
            Assert.IsTrue(result.Contains("Приготовано бургер"));
            Assert.IsTrue(result.Contains("булочка"));
        }

        [TestMethod]
        public void ManageProject_ValidManager_ReturnsProjectInfo()
        {
            // Arrange
            var manager = new Manager("Петро", "IT", 5);
            string projectName = "Новий веб-сайт";

            // Act
            var result = _service.ManageProject(manager, projectName);

            // Assert
            Assert.IsTrue(result.Contains("Керую проектом"));
            Assert.IsTrue(result.Contains(projectName));
            Assert.IsTrue(result.Contains("IT"));
        }

        [TestMethod]
        public void ConductMeeting_ValidManager_ReturnsMeetingInfo()
        {
            // Arrange
            var manager = new Manager("Марія", "HR", 3);

            // Act
            var result = _service.ConductMeeting(manager);

            // Assert
            Assert.IsTrue(result.Contains("Проводжу нараду"));
            Assert.IsTrue(result.Contains("HR"));
        }
    }
}
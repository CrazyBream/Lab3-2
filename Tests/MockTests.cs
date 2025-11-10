using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BLL.Services;
using DAL;
using DAL.DataProviders;
using Core.Entities;
using System.Collections.Generic;
using BLL.Exceptions;

namespace Tests
{
    [TestClass]
    public class MockTests
    {
        private Mock<IParticipantContext> _mockContext;
        private Mock<IDataProvider> _mockDataProvider;
        private Mock<IValidationService> _mockValidationService;
        private ParticipantService _participantService;

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<IParticipantContext>();
            _mockDataProvider = new Mock<IDataProvider>();
            _mockValidationService = new Mock<IValidationService>();

            _participantService = new ParticipantService(
                _mockContext.Object,
                _mockDataProvider.Object,
                _mockValidationService.Object
            );
        }

        [TestMethod]
        public void CountStudentsFromUkraineInThirdCourse_WithMock_ReturnsCorrectCount()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678"),
                new Student("Петренко", 3, "AB654321", 90, "Україна", "ZB87654321")
            };

            var expectedStudents = new List<Student>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678"),
                new Student("Петренко", 3, "AB654321", 90, "Україна", "ZB87654321")
            };

            _mockContext
                .Setup(x => x.GetStudentsFromUkraineInThirdCourse(participants))
                .Returns(expectedStudents);

            // Act
            int result = _participantService.CountStudentsFromUkraineInThirdCourse(participants);

            // Assert
            Assert.AreEqual(2, result);
            _mockContext.Verify(x => x.GetStudentsFromUkraineInThirdCourse(participants), Times.Once);
        }

        [TestMethod]
        public void GetStudentsFromUkraineInThirdCourse_WithMock_ReturnsStudents()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678"),
                new Manager("Менеджер", "IT", 5)
            };

            var expectedStudents = new List<Student>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678")
            };

            _mockContext
                .Setup(x => x.GetStudentsFromUkraineInThirdCourse(participants))
                .Returns(expectedStudents);

            // Act
            var result = _participantService.GetStudentsFromUkraineInThirdCourse(participants);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Іваненко", result[0].LastName);
            _mockContext.Verify(x => x.GetStudentsFromUkraineInThirdCourse(participants), Times.Once);
        }

        [TestMethod]
        public void PrepareBurger_WithMock_CallsValidationAndReturnsResult()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "Касир", 100);
            string[] ingredients = { "булочка", "котлета", "сир" };

            _mockValidationService
                .Setup(x => x.ValidateMсdonaldsWorker(worker))
                .Verifiable();

            // Act
            var result = _participantService.PrepareBurger(worker, ingredients);

            // Assert
            Assert.IsTrue(result.Contains("Приготовано бургер"));
            _mockValidationService.Verify(x => x.ValidateMсdonaldsWorker(worker), Times.Once);
        }

        [TestMethod]
        public void PrepareSpecialBurger_WithMock_CallsValidationAndReturnsResult()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Олена", "Кухар", 200);

            _mockValidationService
                .Setup(x => x.ValidateMсdonaldsWorker(worker))
                .Verifiable();

            // Act
            var result = _participantService.PrepareSpecialBurger(worker);

            // Assert
            Assert.IsTrue(result.Contains("Приготовано бургер"));
            _mockValidationService.Verify(x => x.ValidateMсdonaldsWorker(worker), Times.Once);
        }

        [TestMethod]
        public void ManageProject_WithMock_CallsValidationAndReturnsResult()
        {
            // Arrange
            var manager = new Manager("Петро", "IT", 5);
            string projectName = "Новий проект";

            _mockValidationService
                .Setup(x => x.ValidateManager(manager))
                .Verifiable();

            // Act
            var result = _participantService.ManageProject(manager, projectName);

            // Assert
            Assert.IsTrue(result.Contains("Керую проектом"));
            _mockValidationService.Verify(x => x.ValidateManager(manager), Times.Once);
        }

        [TestMethod]
        public void ConductMeeting_WithMock_CallsValidationAndReturnsResult()
        {
            // Arrange
            var manager = new Manager("Марія", "HR", 3);

            _mockValidationService
                .Setup(x => x.ValidateManager(manager))
                .Verifiable();

            // Act
            var result = _participantService.ConductMeeting(manager);

            // Assert
            Assert.IsTrue(result.Contains("Проводжу нараду"));
            _mockValidationService.Verify(x => x.ValidateManager(manager), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void PrepareBurger_InvalidWorker_ThrowsException()
        {
            // Arrange
            var worker = new MсdonaldsWorker("", "Касир", 100); // Неправильне ім'я
            string[] ingredients = { "булочка", "котлета" };

            _mockValidationService
                .Setup(x => x.ValidateMсdonaldsWorker(worker))
                .Throws(new InvalidParticipantDataException("Ім'я не може бути порожнім"));

            // Act
            _participantService.PrepareBurger(worker, ingredients);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ManageProject_InvalidManager_ThrowsException()
        {
            // Arrange
            var manager = new Manager("", "IT", 5); // Неправильне ім'я
            string projectName = "Проект";

            _mockValidationService
                .Setup(x => x.ValidateManager(manager))
                .Throws(new InvalidParticipantDataException("Ім'я не може бути порожнім"));

            // Act
            _participantService.ManageProject(manager, projectName);
        }

        [TestMethod]
        public void SaveParticipantsToFile_WithMock_CallsDataProvider()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678")
            };
            string filename = "test_file";

            _mockValidationService
                .Setup(x => x.ValidateFileName(filename))
                .Returns(true);

            _mockContext
                .Setup(x => x.SaveParticipants(participants, filename))
                .Verifiable();

            // Act
            _participantService.SaveParticipantsToFile(participants, filename);

            // Assert
            _mockValidationService.Verify(x => x.ValidateFileName(filename), Times.Once);
            _mockContext.Verify(x => x.SaveParticipants(participants, filename), Times.Once);
        }

        [TestMethod]
        public void LoadParticipantsFromFile_WithMock_ReturnsParticipants()
        {
            // Arrange
            string filename = "test_file.json";
            var expectedParticipants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678"),
                new Manager("Петро", "IT", 5)
            };

            _mockValidationService
                .Setup(x => x.ValidateFileName("test_file"))
                .Returns(true);

            _mockContext
                .Setup(x => x.LoadParticipants(filename))
                .Returns(expectedParticipants);

            // Act
            var result = _participantService.LoadParticipantsFromFile(filename);

            // Assert
            Assert.AreEqual(2, result.Count);
            _mockValidationService.Verify(x => x.ValidateFileName("test_file"), Times.Once);
            _mockContext.Verify(x => x.LoadParticipants(filename), Times.Once);
        }

        [TestMethod]
        public void GetChessPlayers_WithMock_ReturnsPlayers()
        {
            // Arrange
            var participants = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678") { CanPlayChess = true },
                new Manager("Петро", "IT", 5) { CanPlayChess = false },
                new MсdonaldsWorker("Олена", "Касир", 100) { CanPlayChess = true }
            };

            var expectedPlayers = new List<Person>
            {
                new Student("Іваненко", 3, "AB123456", 80, "Україна", "ZB12345678") { CanPlayChess = true },
                new MсdonaldsWorker("Олена", "Касир", 100) { CanPlayChess = true }
            };

            _mockContext
                .Setup(x => x.GetChessPlayers(participants))
                .Returns(expectedPlayers);

            // Act
            var result = _participantService.GetChessPlayers(participants);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(p => p.CanPlayChess));
            _mockContext.Verify(x => x.GetChessPlayers(participants), Times.Once);
        }

        [TestMethod]
        public void SetDataProvider_WithMock_CallsValidation()
        {
            // Arrange
            string format = "json";

            _mockValidationService
                .Setup(x => x.ValidateFileFormat(format))
                .Returns(true);

            // Act
            _participantService.SetDataProvider(format);

            // Assert
            _mockValidationService.Verify(x => x.ValidateFileFormat(format), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void SetDataProvider_InvalidFormat_ThrowsException()
        {
            // Arrange
            string format = "txt";

            _mockValidationService
                .Setup(x => x.ValidateFileFormat(format))
                .Returns(false);

            // Act
            _participantService.SetDataProvider(format);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services;
using Core.Entities;
using BLL.Exceptions;

namespace Lab3_2.Tests
{
    [TestClass]
    public class ValidationServiceTests
    {
        private ValidationService _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new ValidationService();
        }

        [TestMethod]
        public void ValidateStudent_ValidData_Passes()
        {
            // Arrange
            var student = new Student("Іваненко", 3, "AB123456", 85, "Україна", "ZB12345678");

            // Act & Assert
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        public void ValidateStudent_BoundaryValues_Passes()
        {
            // Arrange & Act & Assert - не повинно бути винятків
            _validator.ValidateStudent(new Student("Тест", 1, "AB123456", 0, "Україна", "ZB12345678"));
            _validator.ValidateStudent(new Student("Тест", 6, "AB123456", 100, "Україна", "ZB12345678"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_InvalidCourse_ThrowsException()
        {
            // Arrange
            var student = new Student("Іваненко", 7, "AB123456", 85, "Україна", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_ZeroCourse_ThrowsException()
        {
            // Arrange
            var student = new Student("Іваненко", 0, "AB123456", 85, "Україна", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_NegativeAverageGrade_ThrowsException()
        {
            // Arrange
            var student = new Student("Іваненко", 3, "AB123456", -5, "Україна", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_TooHighAverageGrade_ThrowsException()
        {
            // Arrange
            var student = new Student("Іваненко", 3, "AB123456", 105, "Україна", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_InvalidStudentId_ThrowsException()
        {
            // Arrange
            var student = new Student("Іваненко", 3, "INVALID", 85, "Україна", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_InvalidGradeBookNumber_ThrowsException()
        {
            // Arrange
            var student = new Student("Іваненко", 3, "AB123456", 85, "Україна", "INVALID");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_EmptyLastName_ThrowsException()
        {
            // Arrange
            var student = new Student("", 3, "AB123456", 85, "Україна", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_NullLastName_ThrowsException()
        {
            // Arrange
            var student = new Student(null, 3, "AB123456", 85, "Україна", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateStudent_InvalidCountry_ThrowsException()
        {
            // Arrange
            var student = new Student("Іваненко", 3, "AB123456", 85, "USA123", "ZB12345678");

            // Act
            _validator.ValidateStudent(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateManager_EmptyName_ThrowsException()
        {
            // Arrange
            var manager = new Manager("", "HR", 3);

            // Act
            _validator.ValidateManager(manager);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateManager_NullName_ThrowsException()
        {
            // Arrange
            var manager = new Manager(null, "HR", 3);

            // Act
            _validator.ValidateManager(manager);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateManager_InvalidName_ThrowsException()
        {
            // Arrange
            var manager = new Manager("John123", "HR", 3);

            // Act
            _validator.ValidateManager(manager);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateManager_EmptyDepartment_ThrowsException()
        {
            // Arrange
            var manager = new Manager("Іван", "", 3);

            // Act
            _validator.ValidateManager(manager);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateManager_NegativeExperience_ThrowsException()
        {
            // Arrange
            var manager = new Manager("Іван", "HR", -1);

            // Act
            _validator.ValidateManager(manager);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateManager_TooHighExperience_ThrowsException()
        {
            // Arrange
            var manager = new Manager("Іван", "HR", 51);

            // Act
            _validator.ValidateManager(manager);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateMcDonaldsWorker_InvalidHours_ThrowsException()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "Касир", 1000);

            // Act
            _validator.ValidateMсdonaldsWorker(worker);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateMcDonaldsWorker_NegativeHours_ThrowsException()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "Касир", -10);

            // Act
            _validator.ValidateMсdonaldsWorker(worker);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateMcDonaldsWorker_EmptyName_ThrowsException()
        {
            // Arrange
            var worker = new MсdonaldsWorker("", "Касир", 100);

            // Act
            _validator.ValidateMсdonaldsWorker(worker);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParticipantDataException))]
        public void ValidateMcDonaldsWorker_EmptyPosition_ThrowsException()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "", 100);

            // Act
            _validator.ValidateMсdonaldsWorker(worker);
        }

        [TestMethod]
        public void ValidateMcDonaldsWorker_BoundaryHours_Passes()
        {
            // Arrange & Act & Assert - не повинно бути винятків
            _validator.ValidateMсdonaldsWorker(new MсdonaldsWorker("Іван", "Касир", 0));
            _validator.ValidateMсdonaldsWorker(new MсdonaldsWorker("Петро", "Менеджер", 744));
        }

        [TestMethod]
        public void ValidateFileName_ValidName_ReturnsTrue()
        {
            // Act & Assert
            Assert.IsTrue(_validator.ValidateFileName("valid_name"));
            Assert.IsTrue(_validator.ValidateFileName("valid-name"));
            Assert.IsTrue(_validator.ValidateFileName("valid name"));
            Assert.IsTrue(_validator.ValidateFileName("valid123"));
        }

        [TestMethod]
        public void ValidateFileName_InvalidName_ReturnsFalse()
        {
            // Act & Assert
            Assert.IsFalse(_validator.ValidateFileName("invalid|name"));
            Assert.IsFalse(_validator.ValidateFileName("invalid/name"));
            Assert.IsFalse(_validator.ValidateFileName("invalid\\name"));
            Assert.IsFalse(_validator.ValidateFileName("invalid:name"));
            Assert.IsFalse(_validator.ValidateFileName("invalid*name"));
        }

        [TestMethod]
        public void ValidateFileFormat_ValidFormats_ReturnsTrue()
        {
            // Act & Assert
            Assert.IsTrue(_validator.ValidateFileFormat("json"));
            Assert.IsTrue(_validator.ValidateFileFormat("xml"));
            Assert.IsTrue(_validator.ValidateFileFormat("JSON"));
            Assert.IsTrue(_validator.ValidateFileFormat("XML"));
        }

        [TestMethod]
        public void ValidateFileFormat_InvalidFormat_ReturnsFalse()
        {
            // Act & Assert
            Assert.IsFalse(_validator.ValidateFileFormat("txt"));
            Assert.IsFalse(_validator.ValidateFileFormat("csv"));
            Assert.IsFalse(_validator.ValidateFileFormat(""));
            Assert.IsFalse(_validator.ValidateFileFormat(null));
        }
    }
}
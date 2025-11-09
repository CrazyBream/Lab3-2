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
        public void ValidateManager_EmptyName_ThrowsException()
        {
            // Arrange
            var manager = new Manager("", "HR", 3);

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
        public void ValidateFileName_ValidName_ReturnsTrue()
        {
            Assert.IsTrue(_validator.ValidateFileName("valid_name"));
        }

        [TestMethod]
        public void ValidateFileFormat_InvalidFormat_ReturnsFalse()
        {
            Assert.IsFalse(_validator.ValidateFileFormat("txt"));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Entities;

namespace Tests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void MсdonaldsWorker_PrepareBurger_WithIngredients_ReturnsCorrectString()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "Касир", 100);
            string[] ingredients = { "булочка", "котлета", "сир" };

            // Act
            var result = worker.PrepareBurger(ingredients);

            // Assert
            Assert.AreEqual("Приготовано бургер з інгредієнтами: булочка, котлета, сир", result);
        }

        [TestMethod]
        public void MсdonaldsWorker_PrepareBurger_EmptyIngredients_ReturnsErrorMessage()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Іван", "Касир", 100);
            string[] ingredients = { };

            // Act
            var result = worker.PrepareBurger(ingredients);

            // Assert
            Assert.AreEqual("Немає інгредієнтів для приготування бургера", result);
        }

        [TestMethod]
        public void MсdonaldsWorker_PrepareSpecialBurger_ReturnsDefaultIngredients()
        {
            // Arrange
            var worker = new MсdonaldsWorker("Олена", "Кухар", 200);

            // Act
            var result = worker.PrepareSpecialBurger();

            // Assert
            Assert.IsTrue(result.Contains("булочка"));
            Assert.IsTrue(result.Contains("котлета"));
            Assert.IsTrue(result.Contains("сир"));
            Assert.IsTrue(result.Contains("салат"));
            Assert.IsTrue(result.Contains("соус"));
        }

        [TestMethod]
        public void Manager_ManageProject_ValidName_ReturnsProjectInfo()
        {
            // Arrange
            var manager = new Manager("Петро", "IT", 5);
            string projectName = "Новий проект";

            // Act
            var result = manager.ManageProject(projectName);

            // Assert
            Assert.AreEqual("Керую проектом 'Новий проект' у відділі IT (досвід: 5 років)", result);
        }

        [TestMethod]
        public void Manager_ConductMeeting_ReturnsMeetingInfo()
        {
            // Arrange
            var manager = new Manager("Марія", "HR", 3);

            // Act
            var result = manager.ConductMeeting();

            // Assert
            Assert.AreEqual("Проводжу нараду у відділі HR", result);
        }

        [TestMethod]
        public void Manager_MakeDecision_ReturnsDecisionInfo()
        {
            // Arrange
            var manager = new Manager("Петро", "IT", 5);
            string decision = "Запустити новий проект";

            // Act
            var result = manager.MakeDecision(decision);

            // Assert
            Assert.AreEqual("Прийнято рішення: Запустити новий проект (на основі 5 років досвіду)", result);
        }

        [TestMethod]
        public void Manager_EvaluatePerformance_ReturnsCorrectEvaluation()
        {
            // Arrange
            var manager = new Manager("Марія", "HR", 3);

            // Act & Assert
            Assert.AreEqual("Відмінна продуктивність!", manager.EvaluatePerformance(95));
            Assert.AreEqual("Добра продуктивність", manager.EvaluatePerformance(80));
            Assert.AreEqual("Задовільна продуктивність", manager.EvaluatePerformance(65));
            Assert.AreEqual("Потребує покращення", manager.EvaluatePerformance(40));
        }
    }
}
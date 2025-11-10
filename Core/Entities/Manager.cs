using System;
using System.Runtime.Serialization;

namespace Core.Entities
{
    [Serializable]
    [DataContract]
    public class Manager : Person
    {
        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public int Experience { get; set; }

        public Manager() : base("", false) { }

        public Manager(string name, string department, int experience, bool canPlayChess = false)
            : base(name, canPlayChess)
        {
            Department = department;
            Experience = experience;
        }

        public string ManageProject(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return "Назва проекту не може бути порожньою";

            return $"Керую проектом '{projectName}' у відділі {Department} (досвід: {Experience} років)";
        }

        public string ConductMeeting()
        {
            return $"Проводжу нараду у відділі {Department}";
        }

        public string MakeDecision(string decision)
        {
            return $"Прийнято рішення: {decision} (на основі {Experience} років досвіду)";
        }

        public string EvaluatePerformance(int performanceScore)
        {
            if (performanceScore >= 90)
                return "Відмінна продуктивність!";
            else if (performanceScore >= 70)
                return "Добра продуктивність";
            else if (performanceScore >= 50)
                return "Задовільна продуктивність";
            else
                return "Потребує покращення";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Менеджер: {Name}");
            Console.WriteLine($"  Відділ: {Department}");
            Console.WriteLine($"  Досвід (років): {Experience}");
            Console.WriteLine($"  Грає в шахи: {(CanPlayChess ? "Так" : "Ні")}");
        }
    }
}
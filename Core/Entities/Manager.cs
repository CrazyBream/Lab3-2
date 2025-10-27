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

        public override void DisplayInfo()
        {
            Console.WriteLine($"Менеджер: {Name}");
            Console.WriteLine($"  Відділ: {Department}");
            Console.WriteLine($"  Досвід (років): {Experience}");
            Console.WriteLine($"  Грає в шахи: {(CanPlayChess ? "Так" : "Ні")}");
        }
    }
}
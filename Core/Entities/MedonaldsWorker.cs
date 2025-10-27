using System;
using System.Runtime.Serialization;

namespace Core.Entities
{
    [Serializable]
    [DataContract]
    public class MedonaldsWorker : Person
    {
        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public int HoursWorked { get; set; }

        public MedonaldsWorker() : base("", false) { }

        public MedonaldsWorker(string name, string position, int hoursWorked, bool canPlayChess = false)
            : base(name, canPlayChess)
        {
            Position = position;
            HoursWorked = hoursWorked;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Працівник McDonald's: {Name}");
            Console.WriteLine($"  Посада: {Position}");
            Console.WriteLine($"  Відпрацьовано годин: {HoursWorked}");
            Console.WriteLine($"  Грає в шахи: {(CanPlayChess ? "Так" : "Ні")}");
        }
    }
}
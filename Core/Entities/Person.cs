using System;
using System.Runtime.Serialization;

namespace Core.Entities
{
    [Serializable]
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool CanPlayChess { get; set; }

        [DataMember]
        public string Type { get; set; } 

        public Person()
        {
            Type = GetType().Name;
        }

        public Person(string name, bool canPlayChess = false) : this()
        {
            Name = name;
            CanPlayChess = canPlayChess;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Учасник: {Name}");
            Console.WriteLine($"  Грає в шахи: {(CanPlayChess ? "Так" : "Ні")}");
        }
    }
}
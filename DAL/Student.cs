using System;
using System.Runtime.Serialization;

namespace DAL.Entities
{
    [Serializable]
    [DataContract]
    public class Student : Person
    {
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int Course { get; set; }

        [DataMember]
        public string StudentId { get; set; }

        [DataMember]
        public double AverageGrade { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string GradeBookNumber { get; set; }

        public Student() : base() { }

        public Student(string lastName, int course, string studentId, double averageGrade, string country, string gradeBookNumber, bool canPlayChess = false)
            : base($"{lastName} Student", canPlayChess)
        {
            LastName = lastName;
            Course = course;
            StudentId = studentId;
            AverageGrade = averageGrade;
            Country = country;
            GradeBookNumber = gradeBookNumber;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Студент: {LastName}");
            Console.WriteLine($"  Курс: {Course}");
            Console.WriteLine($"  Студентський квиток: {StudentId}");
            Console.WriteLine($"  Середній бал: {AverageGrade}");
            Console.WriteLine($"  Країна: {Country}");
            Console.WriteLine($"  Залікова книжка: {GradeBookNumber}");
            Console.WriteLine($"  Грає в шахи: {(CanPlayChess ? "Так" : "Ні")}");
        }
    }
}
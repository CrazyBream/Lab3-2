using System;
using System.Runtime.Serialization;

namespace Core.Entities
{
    [Serializable]
    [DataContract]
    public class MсdonaldsWorker : Person
    {
        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public int HoursWorked { get; set; }

        public MсdonaldsWorker() : base("", false) { }

        public MсdonaldsWorker(string name, string position, int hoursWorked, bool canPlayChess = false)
            : base(name, canPlayChess)
        {
            Position = position;
            HoursWorked = hoursWorked;
        }

        public string PrepareBurger(string[] ingredients)
        {
            if (ingredients == null || ingredients.Length == 0)
                return "Немає інгредієнтів для приготування бургера";

            return $"Приготовано бургер з інгредієнтами: {string.Join(", ", ingredients)}";
        }

        public string PrepareSpecialBurger()
        {
            string[] defaultIngredients = { "булочка", "котлета", "сир", "салат", "соус" };
            return PrepareBurger(defaultIngredients);
        }

        public string PrepareBurgerByPosition()
        {
            return Position.ToLower() switch
            {
                "касир" => PrepareBurger(new[] { "булочка", "котлета", "сир" }),
                "кухар" => PrepareSpecialBurger(),
                "менеджер" => PrepareBurger(new[] { "булочка", "подвійна котлета", "сир", "бекон", "соус" }),
                _ => PrepareSpecialBurger()
            };
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
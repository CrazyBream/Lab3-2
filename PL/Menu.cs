using System;
using System.Collections.Generic;
using BLL.Services;
using Core.Entities;

namespace PL
{
    public class Menu
    {
        private ParticipantService _participantService;
        private List<Person> _participants;

        public Menu(ParticipantService participantService)
        {
            _participantService = participantService;
            _participants = new List<Person>();
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Система управління учасниками ===");
                Console.WriteLine("1. Управління учасниками");
                Console.WriteLine("2. Студенти");
                Console.WriteLine("3. Гра в шахи");
                Console.WriteLine("4. Робота з файлами");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowParticipantsMenu();
                            break;
                        case "2":
                            ShowStudentsMenu();
                            break;
                        case "3":
                            ShowChessMenu();
                            break;
                        case "4":
                            ShowFilesMenu();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Невірний вибір!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
            }
        }

        private void ShowParticipantsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління учасниками ===");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Додати працівника McDonald's");
                Console.WriteLine("3. Додати менеджера");
                Console.WriteLine("4. Переглянути всіх учасників");
                Console.WriteLine("5. Переглянути гравців у шахи");
                Console.WriteLine("0. Повернутися до головного меню");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        AddMedonaldsWorker();
                        break;
                    case "3":
                        AddManager();
                        break;
                    case "4":
                        DisplayAllParticipants();
                        break;
                    case "5":
                        DisplayChessPlayers();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowStudentsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Робота зі студентами ===");
                Console.WriteLine("1. Обчислити кількість студентів 3-го курсу з України");
                Console.WriteLine("2. Показати студентів 3-го курсу з України");
                Console.WriteLine("0. Повернутися до головного меню");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CountUkrainianThirdCourseStudents();
                        break;
                    case "2":
                        ShowUkrainianThirdCourseStudents();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowChessMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Гра в шахи ===");
                Console.WriteLine("1. Зіграти партію в шахи");
                Console.WriteLine("2. Переглянути гравців у шахи");
                Console.WriteLine("0. Повернутися до головного меню");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayChessGame();
                        break;
                    case "2":
                        DisplayChessPlayers();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowFilesMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Робота з файлами ===");
                Console.WriteLine($"Поточний формат: {_participantService.GetCurrentFileExtension().ToUpper()}");
                Console.WriteLine("1. Зберегти дані у файл");
                Console.WriteLine("2. Завантажити дані з файлу");
                Console.WriteLine("3. Змінити формат збереження");
                Console.WriteLine("0. Повернутися до головного меню");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SaveToFile();
                        break;
                    case "2":
                        LoadFromFile();
                        break;
                    case "3":
                        ChangeFileFormat();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        private void AddStudent()
        {
            Console.WriteLine("\n=== Додавання студента ===");

            try
            {
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine();

                Console.Write("Курс (1-6): ");
                if (!int.TryParse(Console.ReadLine(), out int course) || course < 1 || course > 6)
                {
                    Console.WriteLine("Помилка: Курс повинен бути числом від 1 до 6");
                    return;
                }

                Console.Write("Студентський квиток (формат: AB123456): ");
                string studentId = Console.ReadLine();

                Console.Write("Середній бал (0-100): ");
                if (!double.TryParse(Console.ReadLine(), out double averageGrade) || averageGrade < 0 || averageGrade > 100)
                {
                    Console.WriteLine("Помилка: Середній бал повинен бути числом від 0 до 100");
                    return;
                }

                Console.Write("Країна: ");
                string country = Console.ReadLine();

                Console.Write("Номер залікової книжки (формат: ZB12345678): ");
                string gradeBookNumber = Console.ReadLine();

                Console.Write("Грає в шахи? (так/ні): ");
                bool canPlayChess = Console.ReadLine().ToLower() == "так";

                var student = new Student(lastName, course, studentId, averageGrade, country, gradeBookNumber, canPlayChess);

                _participantService.ValidateStudent(student);
                _participants.Add(student);

                Console.WriteLine(" Студента додано успішно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Помилка: {ex.Message}");
            }
        }
        private void AddMedonaldsWorker()
        {
            Console.WriteLine("\n=== Додавання працівника McDonald's ===");

            try
            {
                Console.Write("Ім'я: ");
                string name = Console.ReadLine();

                Console.Write("Посада: ");
                string position = Console.ReadLine();

                Console.Write("Відпрацьовано годин (0-744): ");
                if (!int.TryParse(Console.ReadLine(), out int hoursWorked) || hoursWorked < 0 || hoursWorked > 744)
                {
                    Console.WriteLine("Помилка: Години роботи повинні бути від 0 до 744");
                    return;
                }

                Console.Write("Грає в шахи? (так/ні): ");
                bool canPlayChess = Console.ReadLine().ToLower() == "так";

                var worker = new MedonaldsWorker(name, position, hoursWorked, canPlayChess);

                _participantService.ValidateMedonaldsWorker(worker);
                _participants.Add(worker);

                Console.WriteLine(" Працівника додано успішно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Помилка: {ex.Message}");
            }
        }

        private void AddManager()
        {
            Console.WriteLine("\n=== Додавання менеджера ===");

            try
            {
                Console.Write("Ім'я: ");
                string name = Console.ReadLine();

                Console.Write("Відділ: ");
                string department = Console.ReadLine();

                Console.Write("Досвід (років, 0-50): ");
                if (!int.TryParse(Console.ReadLine(), out int experience) || experience < 0 || experience > 50)
                {
                    Console.WriteLine("Помилка: Досвід повинен бути від 0 до 50 років");
                    return;
                }

                Console.Write("Грає в шахи? (так/ні): ");
                bool canPlayChess = Console.ReadLine().ToLower() == "так";

                var manager = new Manager(name, department, experience, canPlayChess);

                _participantService.ValidateManager(manager);
                _participants.Add(manager);

                Console.WriteLine(" Менеджера додано успішно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Помилка: {ex.Message}");
            }
        }

        private void DisplayAllParticipants()
        {
            Console.WriteLine("\n=== Список всіх учасників ===");
            if (_participants.Count == 0)
            {
                Console.WriteLine("Учасників немає.");
                return;
            }

            foreach (var participant in _participants)
            {
                participant.DisplayInfo();
                Console.WriteLine(new string('-', 30));
            }
        }

        private void DisplayChessPlayers()
        {
            var chessPlayers = _participantService.GetChessPlayers(_participants);

            Console.WriteLine("\n=== Гравці у шахи ===");
            if (chessPlayers.Count == 0)
            {
                Console.WriteLine("Гравців у шахи не знайдено.");
                return;
            }

            foreach (var player in chessPlayers)
            {
                player.DisplayInfo();
                Console.WriteLine(new string('-', 30));
            }
        }

        private void CountUkrainianThirdCourseStudents()
        {
            int count = _participantService.CountStudentsFromUkraineInThirdCourse(_participants);
            Console.WriteLine($"\nКількість студентів 3-го курсу з України: {count}");
        }

        private void ShowUkrainianThirdCourseStudents()
        {
            var ukrainianStudents = _participantService.GetStudentsFromUkraineInThirdCourse(_participants);

            Console.WriteLine("\n=== Студенти 3-го курсу з України ===");
            if (ukrainianStudents.Count == 0)
            {
                Console.WriteLine("Студентів не знайдено.");
                return;
            }

            foreach (var student in ukrainianStudents)
            {
                student.DisplayInfo();
                Console.WriteLine(new string('-', 30));
            }
        }

        private void PlayChessGame()
        {
            try
            {
                var result = _participantService.PlayChessGame(_participants);

                Console.WriteLine("\n=== Результат гри в шахи ===");
                Console.WriteLine($"Гравець 1: {result.Player1}");
                Console.WriteLine($"Гравець 2: {result.Player2}");
                Console.WriteLine($"Переможець: {result.Winner}");
                Console.WriteLine($"Кількість ходів: {result.Moves}");
                Console.WriteLine(" Вітаємо переможця!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private void ChangeFileFormat()
        {
            Console.WriteLine("\n=== Зміна формату збереження ===");
            var formats = _participantService.GetSupportedFormats();

            Console.WriteLine("Доступні формати:");
            for (int i = 0; i < formats.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {formats[i].ToUpper()}");
            }

            Console.Write("Виберіть формат: ");
            string choice = Console.ReadLine();

            try
            {
                string format = choice.ToLower() switch
                {
                    "1" or "json" => "json",
                    "2" or "xml" => "xml",
                    _ => throw new Exception("Невірний вибір формату")
                };

                _participantService.SetDataProvider(format);
                Console.WriteLine($"Формат змінено на: {format.ToUpper()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private void SaveToFile()
        {
            try
            {
                Console.Write("Введіть ім'я файлу (тільки латинські літери, цифри, _ -): ");
                string filename = Console.ReadLine();

                if (!_participantService.ValidateFileName(filename))
                {
                    Console.WriteLine(" Назва файлу містить недопустимі символи!");
                    return;
                }

                _participantService.SaveParticipantsToFile(_participants, filename);
                string extension = _participantService.GetCurrentFileExtension();
                Console.WriteLine($" Дані збережено у файл: {filename}{extension}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Помилка збереження: {ex.Message}");
            }
        }

        private void LoadFromFile()
        {
            try
            {
                Console.Write("Введіть повне ім'я файлу (з розширенням .json або .xml): ");
                string filename = Console.ReadLine();

                _participants = _participantService.LoadParticipantsFromFile(filename);
                Console.WriteLine($"Дані завантажено з файлу: {filename}");
                Console.WriteLine($"Завантажено {_participants.Count} учасників");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження: {ex.Message}");
                Console.WriteLine("Перевірте формат файлу та спробуйте ще раз.");
            }
        }
    }
}
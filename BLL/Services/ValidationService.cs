using System.Text.RegularExpressions;
using BLL.Exceptions;
using Core.Entities;

namespace BLL.Services
{
    public class ValidationService
    {
        public void ValidateStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.LastName))
                throw new InvalidParticipantDataException("Прізвище не може бути порожнім");

            if (!Regex.IsMatch(student.LastName, @"^[a-zA-Zа-яА-ЯїЇіІєЄґҐ'\- ]+$"))
                throw new InvalidParticipantDataException("Прізвище містить недопустимі символи");

            if (student.Course < 1 || student.Course > 6)
                throw new InvalidParticipantDataException("Курс повинен бути від 1 до 6");

            if (student.AverageGrade < 0 || student.AverageGrade > 100)
                throw new InvalidParticipantDataException("Середній бал повинен бути від 0 до 100");

            if (!Regex.IsMatch(student.StudentId, @"^[A-Z]{2}\d{6}$"))
                throw new InvalidParticipantDataException("Студентський квиток повинен мати формат: AB123456");

            if (!Regex.IsMatch(student.Country, @"^[a-zA-Zа-яА-ЯїЇіІєЄґҐ'\- ]+$"))
                throw new InvalidParticipantDataException("Назва країни містить недопустимі символи");

            if (!Regex.IsMatch(student.GradeBookNumber, @"^[A-Z]{2}\d{8}$"))
                throw new InvalidParticipantDataException("Номер залікової книжки повинен мати формат: ZB12345678");
        }

        public void ValidateMedonaldsWorker(MedonaldsWorker worker)
        {
            if (string.IsNullOrWhiteSpace(worker.Name))
                throw new InvalidParticipantDataException("Ім'я не може бути порожнім");

            if (!Regex.IsMatch(worker.Name, @"^[a-zA-Zа-яА-ЯїЇіІєЄґҐ'\- ]+$"))
                throw new InvalidParticipantDataException("Ім'я містить недопустимі символи");

            if (string.IsNullOrWhiteSpace(worker.Position))
                throw new InvalidParticipantDataException("Посада не може бути порожньою");

            if (worker.HoursWorked < 0 || worker.HoursWorked > 744) 
                throw new InvalidParticipantDataException("Години роботи повинні бути від 0 до 744");
        }

        public void ValidateManager(Manager manager)
        {
            if (string.IsNullOrWhiteSpace(manager.Name))
                throw new InvalidParticipantDataException("Ім'я не може бути порожнім");

            if (!Regex.IsMatch(manager.Name, @"^[a-zA-Zа-яА-ЯїЇіІєЄґҐ'\- ]+$"))
                throw new InvalidParticipantDataException("Ім'я містить недопустимі символи");

            if (string.IsNullOrWhiteSpace(manager.Department))
                throw new InvalidParticipantDataException("Відділ не може бути порожнім");

            if (manager.Experience < 0 || manager.Experience > 50)
                throw new InvalidParticipantDataException("Досвід повинен бути від 0 до 50 років");
        }

        public bool ValidateFileName(string filename)
        {
            return Regex.IsMatch(filename, @"^[a-zA-Z0-9_\- ]+$");
        }

        public bool ValidateFileFormat(string format)
        {
            return format.ToLower() == "json" || format.ToLower() == "xml";
        }
    }
}
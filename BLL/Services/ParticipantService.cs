using BLL.Exceptions;
using BLL;
using DAL;
using DAL.DataProviders;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class ParticipantService : IParticipantService
    {
        private ParticipantContext _context;
        private Random _random;
        private IDataProvider _currentDataProvider;
        private ValidationService _validationService;

        public ParticipantService(ParticipantContext context)
        {
            _context = context;
            _random = new Random();
            _currentDataProvider = new JsonDataProvider();
            _validationService = new ValidationService();
        }

        public void SetDataProvider(string format)
        {
            if (!_validationService.ValidateFileFormat(format))
                throw new InvalidParticipantDataException("Непідтримуваний формат. Використовуйте 'json' або 'xml'");

            _currentDataProvider = format.ToLower() switch
            {
                "json" => new JsonDataProvider(),
                "xml" => new XmlDataProvider(),
                _ => throw new InvalidParticipantDataException("Непідтримуваний формат")
            };
        }

        public string GetCurrentFileExtension() => _currentDataProvider.FileExtension;
        public List<string> GetSupportedFormats() => new List<string> { "json", "xml" };

        public void ValidateStudent(Student student) => _validationService.ValidateStudent(student);
        public void ValidateMedonaldsWorker(MedonaldsWorker worker) => _validationService.ValidateMedonaldsWorker(worker);
        public void ValidateManager(Manager manager) => _validationService.ValidateManager(manager);
        public bool ValidateFileName(string filename) => _validationService.ValidateFileName(filename);

        public int CountStudentsFromUkraineInThirdCourse(List<Person> participants)
        {
            var ukrainianStudents = _context.GetStudentsFromUkraineInThirdCourse(participants);
            return ukrainianStudents.Count;
        }

        public List<Student> GetStudentsFromUkraineInThirdCourse(List<Person> participants)
        {
            return _context.GetStudentsFromUkraineInThirdCourse(participants);
        }

        public List<Person> GetChessPlayers(List<Person> participants)
        {
            return _context.GetChessPlayers(participants);
        }

        public ChessGameResult PlayChessGame(List<Person> participants)
        {
            var chessPlayers = GetChessPlayers(participants);

            if (chessPlayers.Count < 2)
                throw new ChessGameException("Для гри потрібно щонайменше 2 гравці!");

            var shuffledPlayers = chessPlayers.OrderBy(x => _random.Next()).ToList();
            var player1 = shuffledPlayers[0];
            var player2 = shuffledPlayers[1];

            string winner = _random.Next(2) == 0 ? player1.Name : player2.Name;
            int moves = _random.Next(20, 60);

            return new ChessGameResult(player1.Name, player2.Name, winner, moves);
        }

        public void SaveParticipantsToFile(List<Person> participants, string filename)
        {
            try
            {
                if (!_validationService.ValidateFileName(filename))
                    throw new InvalidParticipantDataException("Назва файлу містить недопустимі символи");

                _context.SetDataProvider(_currentDataProvider);
                _context.SaveParticipants(participants, filename);
            }
            catch (Exception ex)
            {
                throw new FileOperationException($"Помилка збереження файлу: {ex.Message}");
            }
        }

        public List<Person> LoadParticipantsFromFile(string filename)
        {
            try
            {
                if (!_validationService.ValidateFileName(Path.GetFileNameWithoutExtension(filename)))
                    throw new InvalidParticipantDataException("Назва файлу містить недопустимі символи");


                if (filename.EndsWith(".json"))
                    _currentDataProvider = new JsonDataProvider();
                else if (filename.EndsWith(".xml"))
                    _currentDataProvider = new XmlDataProvider();

                _context.SetDataProvider(_currentDataProvider);
                return _context.LoadParticipants(filename);
            }
            catch (Exception ex)
            {
                throw new FileOperationException($"Помилка завантаження файлу: {ex.Message}");
            }
        }
    }
}
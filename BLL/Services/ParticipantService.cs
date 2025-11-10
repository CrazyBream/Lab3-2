using BLL.Exceptions;
using BLL;
using DAL;
using DAL.DataProviders;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BLL.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantContext _context;
        private IDataProvider _dataProvider;
        private readonly IValidationService _validationService;
        private Random _random;

        public ParticipantService(IParticipantContext context, IDataProvider dataProvider, IValidationService validationService)
        {
            _context = context;
            _dataProvider = dataProvider;
            _validationService = validationService;
            _random = new Random();
        }

        public ParticipantService(ParticipantContext context)
            : this(context, new JsonDataProvider(), new ValidationService())
        {
        }

        public void SetDataProvider(string format)
        {
            if (!_validationService.ValidateFileFormat(format))
                throw new InvalidParticipantDataException("Непідтримуваний формат. Використовуйте 'json' або 'xml'");

            IDataProvider newProvider = format.ToLower() switch
            {
                "json" => new JsonDataProvider(),
                "xml" => new XmlDataProvider(),
                _ => throw new InvalidParticipantDataException("Непідтримуваний формат")
            };

            _context.SetDataProvider(newProvider);
            _dataProvider = newProvider; 
        }

        public string GetCurrentFileExtension() => _dataProvider.FileExtension;

        public List<string> GetSupportedFormats() => new List<string> { "json", "xml" };

        public void ValidateStudent(Student student) => _validationService.ValidateStudent(student);
        public void ValidateMсdonaldsWorker(MсdonaldsWorker worker) => _validationService.ValidateMсdonaldsWorker(worker);
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

                return _context.LoadParticipants(filename);
            }
            catch (Exception ex)
            {
                throw new FileOperationException($"Помилка завантаження файлу: {ex.Message}");
            }
        }


        public string PrepareBurger(MсdonaldsWorker worker, string[] ingredients)
        {
            _validationService.ValidateMсdonaldsWorker(worker);
            return worker.PrepareBurger(ingredients);
        }

        public string PrepareSpecialBurger(MсdonaldsWorker worker)
        {
            _validationService.ValidateMсdonaldsWorker(worker);
            return worker.PrepareSpecialBurger();
        }

        public string ManageProject(Manager manager, string projectName)
        {
            _validationService.ValidateManager(manager);
            return manager.ManageProject(projectName);
        }

        public string ConductMeeting(Manager manager)
        {
            _validationService.ValidateManager(manager);
            return manager.ConductMeeting();
        }
    }
}
using System.Collections.Generic;
using Core.Entities;

namespace BLL
{
    public interface IParticipantService
    {
        void SetDataProvider(string format);
        string GetCurrentFileExtension();
        List<string> GetSupportedFormats();

        void ValidateStudent(Student student);
        void ValidateMсdonaldsWorker(MсdonaldsWorker worker);
        void ValidateManager(Manager manager);

        int CountStudentsFromUkraineInThirdCourse(List<Person> participants);
        List<Student> GetStudentsFromUkraineInThirdCourse(List<Person> participants);
        List<Person> GetChessPlayers(List<Person> participants);

        ChessGameResult PlayChessGame(List<Person> participants);

        void SaveParticipantsToFile(List<Person> participants, string filename);
        List<Person> LoadParticipantsFromFile(string filename);
    }
}
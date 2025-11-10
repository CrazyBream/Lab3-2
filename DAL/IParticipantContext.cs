using System.Collections.Generic;
using Core.Entities;

namespace DAL
{
    public interface IParticipantContext
    {
        void SetDataProvider(IDataProvider dataProvider);
        void SaveParticipants(List<Person> participants, string filename);
        List<Person> LoadParticipants(string filename);
        List<Student> GetStudentsFromUkraineInThirdCourse(List<Person> participants);
        List<Person> GetChessPlayers(List<Person> participants);
    }
}
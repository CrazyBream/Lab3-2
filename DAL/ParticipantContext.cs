using System.Collections.Generic;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL
{
    public class ParticipantContext
    {
        private IDataProvider _dataProvider;

        public ParticipantContext(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void SetDataProvider(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void SaveParticipants(List<Person> participants, string filename)
        {
            _dataProvider.SaveParticipants(participants, filename);
        }

        public List<Person> LoadParticipants(string filename)
        {
            return _dataProvider.LoadParticipants(filename);
        }

        public List<Student> GetStudentsFromUkraineInThirdCourse(List<Person> participants)
        {
            var result = new List<Student>();
            foreach (var person in participants)
            {
                if (person is Student student && student.Course == 3 && student.Country.ToLower() == "україна")
                {
                    result.Add(student);
                }
            }
            return result;
        }

        public List<Person> GetChessPlayers(List<Person> participants)
        {
            var result = new List<Person>();
            foreach (var person in participants)
            {
                if (person.CanPlayChess)
                {
                    result.Add(person);
                }
            }
            return result;
        }
    }
}
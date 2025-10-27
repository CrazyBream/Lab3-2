using System.Collections.Generic;
using Core.Entities;

namespace DAL
{
    public interface IDataProvider
    {
        void SaveParticipants(List<Person> participants, string filename);
        List<Person> LoadParticipants(string filename);
        string FileExtension { get; }
    }
}
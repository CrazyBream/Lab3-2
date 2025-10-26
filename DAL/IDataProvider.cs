using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IDataProvider
    {
        void SaveParticipants(List<Person> participants, string filename);
        List<Person> LoadParticipants(string filename);
        string FileExtension { get; }
    }
}
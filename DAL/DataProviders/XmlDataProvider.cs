using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Core.Entities;
using DAL;

namespace DAL.DataProviders
{
    public class XmlDataProvider : IDataProvider
    {
        public string FileExtension => ".xml";

        public void SaveParticipants(List<Person> participants, string filename)
        {
            if (!filename.EndsWith(FileExtension))
                filename += FileExtension;

            var serializer = new XmlSerializer(typeof(List<Person>), new[] {
                typeof(Student),
                typeof(MedonaldsWorker),
                typeof(Manager)
            });

            using (TextWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, participants);
            }
        }

        public List<Person> LoadParticipants(string filename)
        {
            if (!filename.EndsWith(FileExtension))
                filename += FileExtension;

            var serializer = new XmlSerializer(typeof(List<Person>), new[] {
                typeof(Student),
                typeof(MedonaldsWorker),
                typeof(Manager)
            });

            using (TextReader reader = new StreamReader(filename))
            {
                return (List<Person>)serializer.Deserialize(reader);
            }
        }
    }
}
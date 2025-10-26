using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.DataProviders
{
    public class JsonDataProvider : IDataProvider
    {
        public string FileExtension => ".json";

        public void SaveParticipants(List<Person> participants, string filename)
        {
            if (!filename.EndsWith(FileExtension))
                filename += FileExtension;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                Converters = { new PersonConverter() } 
            };

            string jsonString = JsonSerializer.Serialize(participants, options);
            File.WriteAllText(filename, jsonString);
        }

        public List<Person> LoadParticipants(string filename)
        {
            if (!filename.EndsWith(FileExtension))
                filename += FileExtension;

            string jsonString = File.ReadAllText(filename);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new PersonConverter() }
            };

            return JsonSerializer.Deserialize<List<Person>>(jsonString, options);
        }
    }

    public class PersonConverter : JsonConverter<Person>
    {
        public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                if (root.TryGetProperty("Type", out JsonElement typeElement))
                {
                    string typeName = typeElement.GetString();

                    return typeName switch
                    {
                        "Student" => JsonSerializer.Deserialize<Student>(root.GetRawText(), options),
                        "MedonaldsWorker" => JsonSerializer.Deserialize<MedonaldsWorker>(root.GetRawText(), options),
                        "Manager" => JsonSerializer.Deserialize<Manager>(root.GetRawText(), options),
                        _ => JsonSerializer.Deserialize<Person>(root.GetRawText(), options)
                    };
                }

                throw new JsonException("Неможливо визначити тип Person");
            }
        }

        public override void Write(Utf8JsonWriter writer, Person value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Student student:
                    JsonSerializer.Serialize(writer, student, options);
                    break;
                case MedonaldsWorker worker:
                    JsonSerializer.Serialize(writer, worker, options);
                    break;
                case Manager manager:
                    JsonSerializer.Serialize(writer, manager, options);
                    break;
                default:
                    JsonSerializer.Serialize(writer, value, options);
                    break;
            }
        }
    }
}
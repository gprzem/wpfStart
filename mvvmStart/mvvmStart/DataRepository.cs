using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace mvvmStart
{
    class DataRepository : IDataRepository
    {
        static DataRepository()
        {
            string dataDirectoryPath =
                String.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName)
                ));
            if (!Directory.Exists(dataDirectoryPath))
            {
                Directory.CreateDirectory(dataDirectoryPath);
            }
            filePath = String.Format("{0}\\{1}", dataDirectoryPath, fileName);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }
        private static string fileName = "personData.xml";
        private static string filePath;

        public DataRepository()
        {
            _people = deSerializeFileData();
        }

        private List<Person> deSerializeFileData()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                try
                {
                    return (List<Person>)_mySerializer.Deserialize(reader);
                }

                catch
                {
                    return new List<Person>();
                }
            }
        }

        private void SerializeFileData()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                try
                {
                    _mySerializer.Serialize(writer, _people);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Wystąpił błąd podczas zapisu danych: {0}", ex.Message));
                }
            }
        }

        private List<Person> _people;

        private XmlSerializer _mySerializer = new XmlSerializer(typeof(List<Person>), rootAtribute);

        private static XmlRootAttribute rootAtribute = new XmlRootAttribute("Persons");

        public bool CheckIfExist(Person _person)
        {
            if (_people.Exists(p => p == _person))
            {
                return true;
            }
            return false;
        }

        public void SavePerson(Person _person)
        {
            _people.Add(new Person(_person));
            SerializeFileData();
        }
    }
}

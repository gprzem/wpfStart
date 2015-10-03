using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace mvvmStart
{
    [Serializable]
    public class Person
    {
        public Person(Person _person)
        {
            FirstName = _person.FirstName;
            LastName = _person.LastName;
            Address = _person.Address;
        }

        public Person()
        {
        }

        public static bool operator ==(Person first, Person second)
        {
            if (first.FirstName == second.FirstName
                && first.LastName == second.LastName
                && first.Address == second.Address)
                return true;
            return false;
        }

        public static bool operator !=(Person first, Person second)
        {
            if (first.FirstName != second.FirstName
                || first.LastName != second.LastName
                || first.Address != second.Address)
                return true;
            return false;
        }

        [XmlAttribute]
        public string FirstName { get; set; }
        [XmlAttribute]
        public string LastName { get; set; }
        [XmlAttribute]
        public string Address { get; set; }
    }
}

namespace mvvmStart
{
    internal interface IDataRepository
    {
        bool CheckIfExist(Person _person);
        void SavePerson(Person _person);
    }
}
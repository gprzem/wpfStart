using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace mvvmStart
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _person = new Person();
            _repo = new DataRepository();
        }

        private DataRepository _repo;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Person _person;

        public string FirstName
        {
            get { return _person.FirstName; }
            set
            {
                _person.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _person.LastName; }
            set
            {
                _person.LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Address
        {
            get { return _person.Address; }
            set
            {
                _person.Address = value;
                OnPropertyChanged("Address");
            }
        }

        public ICommand SayHi
        {
            get { return new RelayCommand(SayHiExecute, CanSayHiExecute); }
        }

        private void SayHiExecute()
        {
            MessageBox.Show(string.Format("Witaj {0} {1}", _person.FirstName, _person.LastName));
            _repo.SavePerson(_person);
            _person = new Person();
            OnPropertyChanged("FirstName");
            OnPropertyChanged("LastName");
            OnPropertyChanged("Address");
        }


        private bool CanSayHiExecute()
        {
            if (PersonExist(_person))
                return false;
            else
                return true;
        }

        private bool PersonExist(Person _person)
        {
            return _repo.CheckIfExist(_person);
        }
    }
}

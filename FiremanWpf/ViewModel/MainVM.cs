// //Fireman->FiremanWpf->MainVM.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;
using FiremanWpf.Model;

using System.Collections.ObjectModel;

using FiremanWpf.Tools;

using INotifyPropertyChanged = System.ComponentModel.INotifyPropertyChanged;

namespace FiremanWpf.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RelayCommand _updateCommand;


        private IFiremanRepository _db;
        private string _testText;


        public MainVM()
        {
            //_db = new DatabaseRepository(FiremanContext.GetDatabaseConnection("fireman", "fireman", "fire", "firemandbserver7798.cloudapp.net"));
            _db = new JsonRepository("http://fmanapi1213123.azurewebsites.net");
            _db.UpdateData();
        }

        public ObservableCollection<Operator> Operators => _db.Operators;

        public ObservableCollection<Department> Departments => _db.Departments;

        public ObservableCollection<BuildObject> Adresses => _db.Addreses;

        public ObservableCollection<Hydrant> Hydrants => _db.Hydrants;

        public IFiremanRepository Repository => _db;

        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new RelayCommand(o => { _db.UpdateData(); }));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

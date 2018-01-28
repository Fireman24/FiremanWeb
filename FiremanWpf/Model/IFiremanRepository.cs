// //Fireman->FiremanWpf->IFiremanRepository.cs
// //andreygolubkow Андрей Голубков

using System.Collections.ObjectModel;

namespace FiremanWpf.Model
{
    public interface IFiremanRepository
    {
        ObservableCollection<Department> Departments { get; }

        ObservableCollection<Fire> Fires { get; }

        ObservableCollection<FireCar> FireCars { get; }

        ObservableCollection<Hydrant> Hydrants { get; }

        ObservableCollection<Operator> Operators { get; }

        ObservableCollection<BuildObject> Addreses { get; }
        
         void UpdateData();

        
    }
}

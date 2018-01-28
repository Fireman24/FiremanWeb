// //Fireman->FiremanWpf->JsonRepository.cs
// //andreygolubkow Андрей Голубков

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;

using FiremanWpf.Annotations;

using Newtonsoft.Json;

namespace FiremanWpf.Model
{
    public class JsonRepository : IFiremanRepository, INotifyPropertyChanged    
    {
        private ObservableCollection<Department> _departments;
        private ObservableCollection<Fire> _fires;
        private ObservableCollection<FireCar> _fireCars;
        private ObservableCollection<Hydrant> _hydrants;
        private ObservableCollection<BuildObject> _addreses;
        private ObservableCollection<Operator> _operators;
        public static string Url { get; set; }

        public JsonRepository(string url)
        {
            Url = url;
        }

        

        #region Implementation of IFiremanRepository

        /// <inheritdoc />
        public ObservableCollection<Department> Departments
        {
            get
            {
                return _departments;
            }
            private set
            {
                _departments = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public ObservableCollection<Fire> Fires
        {
            get
            {
                return _fires;
            }
            private set
            {

                _fires = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public ObservableCollection<FireCar> FireCars
        {
            get
            {
                return _fireCars;
            }
            private set
            {
                _fireCars = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public ObservableCollection<Hydrant> Hydrants
        {
            get
            {
                return _hydrants;
            }
            private set
            {
                _hydrants = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public ObservableCollection<Operator> Operators
        {
            get
            {
                return _operators;
            }
            private set
            {
                _operators = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public ObservableCollection<BuildObject> Addreses
        {
            get
            {
                return _addreses;
            }
            private set
            {
                _addreses = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public async void UpdateData()
        {
            Departments = await LoadFromUrl<ObservableCollection<Department>>("/api/department");
            Hydrants = await LoadFromUrl<ObservableCollection<Hydrant>>("/api/hydrant"); 
            Operators = await LoadFromUrl<ObservableCollection<Operator>>("/api/operator");
        }

        #endregion

        private Task<T> LoadFromUrl<T>( string url)
        {
            return Task.Run<T>(() =>
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(Url+url).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        var dataReceiver = JsonConvert.DeserializeObject<T>(json);
                        return dataReceiver;
                    }
                    throw new HttpException("");
                }
            });
        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

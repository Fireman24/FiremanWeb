// //Fireman->FiremanWpf->Address.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;

namespace FiremanWpf.Model
{
    public class BuildObject : INotifyPropertyChanged
    {
        private int _id;
        private string _address;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return Address;
        }



        #endregion

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

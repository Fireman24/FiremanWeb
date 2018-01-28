// //FiremanApi->FiremanApi->Image.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;

namespace FiremanWpf.Model
{
    public class Image : INotifyPropertyChanged
    {
        private string _url;
        private string _name;
        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if ( value == _id )
                    return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if ( value == _name )
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if ( value == _url )
                    return;
                _url = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

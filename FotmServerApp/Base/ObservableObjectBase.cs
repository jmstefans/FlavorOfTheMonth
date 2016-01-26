using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FotmServerApp.Base
{
    public abstract class ObservableObjectBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

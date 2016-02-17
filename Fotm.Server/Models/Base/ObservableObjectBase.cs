using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Fotm.Server.Models.Base
{
    /// <summary>
    /// Abstract class that implements PropertyChanged for data binding.
    /// </summary>
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

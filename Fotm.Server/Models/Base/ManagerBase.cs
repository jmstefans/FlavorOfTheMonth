using System;

namespace Fotm.Server.Models.Base
{
    public abstract class ManagerBase<T> where T: ManagerBase<T>, new()
    {
        #region Optional Singleton Pattern

        private static Lazy<T>  _default = new Lazy<T>(() => new T());

        public static T Default => _default.Value;
     
        #endregion
    }
}

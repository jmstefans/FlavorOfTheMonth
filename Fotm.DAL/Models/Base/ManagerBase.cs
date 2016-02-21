using System;

namespace Fotm.DAL.Models.Base
{
    /// <summary>
    /// Base class for all Managemer classes. 
    /// </summary>
    /// <typeparam name="T">The Manager type to create</typeparam>
    public abstract class ManagerBase<T> where T: ManagerBase<T>, new()
    {
        #region Optional Singleton Pattern

        private static Lazy<T>  _default = new Lazy<T>(() => new T());

        public static T Default => _default.Value;
     
        #endregion
    }
}

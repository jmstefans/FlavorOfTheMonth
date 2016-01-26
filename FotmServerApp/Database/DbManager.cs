using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotmServerApp.Database
{
    /// <summary>
    /// Persistent DB class for reading and writing to the database. 
    /// </summary>
    public class DbManager
    {
        #region Singleton Constructor & Instance

        private static DbManager _dbManager;

        public static DbManager Instance => _dbManager ?? (_dbManager = new DbManager());

        private DbManager() { }

        #endregion

        #region Create

        #endregion

        #region Read

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
    }
}

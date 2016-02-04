using System;

namespace FotmServerApp.Models.Base
{
    public abstract class DbEntityBase : ObservableObjectBase
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public char ModifiedStatus { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int ModifiedUserID { get; set; }
    }
}

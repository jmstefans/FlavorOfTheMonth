using System;

namespace FotmServerApp.Models.Base
{
    public abstract class DbEntityBase : ObservableObjectBase
    {
        /// <summary>
        /// The date the entity was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        
        /// <summary>
        /// The status of the modification.
        /// </summary>
        public char ModifiedStatus { get; set; }
        
        /// <summary>
        /// The ID of the user who last modified.
        /// </summary>
        public int ModifiedUserID { get; set; }
    }
}

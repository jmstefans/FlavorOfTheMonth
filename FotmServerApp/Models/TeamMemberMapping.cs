using System;
using FotmServerApp.Models.Base;

namespace FotmServerApp.Models
{
    public class TeamMemberMapping :DbEntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid TeamMemberMappingID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TeamID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TeamMemberId { get; set; }
    }
}

using System;
using Fotm.DAL.Models.Base;

namespace Fotm.DAL.Models
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

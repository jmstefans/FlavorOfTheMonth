using System;
using Fotm.Server.Models.Base;

namespace Fotm.Server.Models
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

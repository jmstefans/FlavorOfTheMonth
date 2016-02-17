using Fotm.Server.Models.Base;

namespace Fotm.Server.Models
{
    public class RatingChange : DbEntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int RatingChangeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CharacterID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RatingChangeValue { get; set; }
    }
}

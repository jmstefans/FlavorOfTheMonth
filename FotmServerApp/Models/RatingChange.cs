using FotmServerApp.Models.Base;

namespace FotmServerApp.Models
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
